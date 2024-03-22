using CherryPicking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace CherryPicking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Initialize a 3x3 grid with random values (-1, 0, 1)
            int[][] initialGrid = new int[3][];
            Random random = new Random();

            for (int i = 0; i < 3; i++)
            {
                initialGrid[i] = new int[3];
                for (int j = 0; j < 3; j++)
                {
                    initialGrid[i][j] = random.Next(-1, 2); // Generates random value: -1, 0, or 1
                }
            }

            var model = new CherryPickerModel
            {
                Grid = initialGrid
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CalculateCherries(CherryPickerModel model)
        {
            model.MaxCherries = CherryPickup(model.Grid);
            return View("Index", model);
        }

        public int CherryPickup(int[][] grid)
        {
            int n = grid.Length;
            int[][][][] dp = new int[n][][][];

            for (int i = 0; i < n; i++)
            {
                dp[i] = new int[n][][];
                for (int j = 0; j < n; j++)
                {
                    dp[i][j] = new int[n][];
                    for (int k = 0; k < n; k++)
                    {
                        dp[i][j][k] = new int[n];
                        for (int l = 0; l < n; l++)
                        {
                            dp[i][j][k][l] = -1;
                        }
                    }
                }
            }

            return Math.Max(0, DFS(grid, dp, 0, 0, 0));
        }

        private int DFS(int[][] grid, int[][][][] dp, int r1, int c1, int r2)
        {
            int n = grid.Length;
            int c2 = r1 + c1 - r2;

            if (r1 >= n || c1 >= n || r2 >= n || c2 >= n || grid[r1][c1] == -1 || grid[r2][c2] == -1)
                return int.MinValue;

            if (r1 == n - 1 && c1 == n - 1)
                return grid[r1][c1];

            if (dp[r1][c1][r2][c2] != -1)
                return dp[r1][c1][r2][c2];

            int cherries = grid[r1][c1];

            if (r1 != r2 || c1 != c2)
                cherries += grid[r2][c2];

            cherries += Math.Max(Math.Max(DFS(grid, dp, r1 + 1, c1, r2), DFS(grid, dp, r1, c1 + 1, r2)),
                                 Math.Max(DFS(grid, dp, r1 + 1, c1, r2 + 1), DFS(grid, dp, r1, c1 + 1, r2 + 1)));

            dp[r1][c1][r2][c2] = cherries;

            return cherries;
        }
    }
}