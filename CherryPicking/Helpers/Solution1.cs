namespace CherryPicking.Helpers
{
    public class Solution1
    {
        private int Helper(int x1, int y1, int y2, int[][] grid, int size, int[,,] dp)
        {
            int x2 = x1 + y1 - y2;

            if (x1 == size || x2 == size || y1 == size || y2 == size || grid[x1][y1] == -1 || grid[x2][y2] == -1)
            {
                return int.MinValue;
            }

            if (x1 == size - 1 && y1 == size - 1)
            {
                return grid[x1][y1];
            }

            if (dp[x1, y1, y2] != -1)
            {
                return dp[x1, y1, y2];
            }

            int ans = grid[x1][y1];

            if (y1 != y2)
            {
                ans += grid[x2][y2];
            }

            ans += Math.Max(Math.Max(Helper(x1, y1 + 1, y2 + 1, grid, size, dp), Helper(x1 + 1, y1, y2 + 1, grid, size, dp)), Math.Max(Helper(x1, y1 + 1, y2, grid, size, dp), Helper(x1 + 1, y1, y2, grid, size, dp)));

            dp[x1, y1, y2] = ans;

            return dp[x1, y1, y2];
        }

        public int CherryPickup(int[][] grid)
        {
            int n = grid.Length;

            int[,,] dp = new int[n + 1, n + 1, n + 1];
            for (int i = 0; i < n + 1; i++)
                for (int j = 0; j < n + 1; j++)
                    for (int k = 0; k < n + 1; k++)
                        dp[i, j, k] = -1;

            return Math.Max(0, Helper(0, 0, 0, grid, grid.Length, dp));
        }
    }
}
