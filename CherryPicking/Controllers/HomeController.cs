using CherryPicking.Helpers;
using CherryPicking.Models;
using Microsoft.AspNetCore.Mvc;


namespace CherryPicking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int gridSize = 3)
        {
            // Initialize a gridSize x gridSize grid with random values (-1, 0, 1)
            int[][] initialGrid = new int[gridSize][];
            Random random = new Random();

            for (int i = 0; i < gridSize; i++)
            {
                initialGrid[i] = new int[gridSize];
                for (int j = 0; j < gridSize; j++)
                {
                    initialGrid[i][j] = random.Next(-1, 2); // Generates random value: -1, 0, or 1
                }
            }

            var model = new CherryPickerModel
            {
                Grid = initialGrid,
                GridSize = gridSize
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CalculateCherries([FromBody] CherryPickerModel request)
        {
            if (request == null || request.Grid == null)
            {
                return BadRequest("Invalid request data.");
            }


            int maxCherries = 0;


            switch (request.SelectedOption)
            {
                case "myOption":
                    maxCherries = CherryPickup(request.Grid);
                    break;
                case "solution1":
                    var solution1 = new Solution1();
                    maxCherries = solution1.CherryPickup(request.Grid);
                    break;
                case "solution2":
                    var solution2 = new Solution2();
                    maxCherries = solution2.CherryPickup(request.Grid);
                    break;
                default:
                    return BadRequest("Invalid option selected.");
            }


            return Json(new { maxCherries });
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