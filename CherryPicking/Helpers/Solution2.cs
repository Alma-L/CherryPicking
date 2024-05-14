namespace CherryPicking.Helpers
{
    public class Solution2
    {
        int?[,,] dp;
        public int CherryPickup(int[][] grid)
        {
            dp = new int?[grid.Length, grid[0].Length, grid.Length];
            return Math.Max(0, maxPickup(grid, 0, 0, 0));
        }
        public int maxPickup(int[][] grid, int r1, int c1, int r2)
        {
            int c2 = r1 + c1 - r2;
            int m = grid.Length, n = grid[0].Length;
            if (r1 < 0 || c1 < 0 || r1 >= m || c1 >= n || r2 < 0 || c2 < 0 || r2 >= m || c2 >= n ||
                grid[r1][c1] == -1 || grid[r2][c2] == -1)
                return int.MinValue;

            if (r1 == grid.Length - 1 && c1 == grid[0].Length - 1) return grid[r1][c1];
            if (r2 == grid.Length - 1 && c2 == grid[0].Length - 1) return grid[r2][c2];

            if (dp[r1, c1, r2] != null) return dp[r1, c1, r2].Value;

            int count = 0;
            if (r1 == r2 && c1 == c2 && grid[r1][c1] == 1)
                count += grid[r1][c1];
            else
                count += grid[r1][c1] + grid[r2][c2];

            int op1 = maxPickup(grid, r1 + 1, c1, r2 + 1);
            int op2 = maxPickup(grid, r1, c1 + 1, r2);
            int op3 = maxPickup(grid, r1 + 1, c1, r2);
            int op4 = maxPickup(grid, r1, c1 + 1, r2 + 1);

            int max = count + new int[] { op1, op2, op3, op4 }.Max();
            dp[r1, c1, r2] = max;
            return max;
        }
    }
}
