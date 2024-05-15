namespace CherryPicking.Models
{

    public class CherryPickerModel
    {
        public int[][] Grid { get; set; }
        public int MaxCherries { get; set; }
        public string SelectedOption { get; set; } = string.Empty;
        public int GridSize { get; set; }

        public CherryPickerModel()
        {
            // Default grid size is 4x4
            GridSize = 3;
            InitializeGrid();
        }

        public CherryPickerModel(int gridSize)
        {
            GridSize = gridSize;
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            Grid = new int[GridSize][];
            for (int i = 0; i < GridSize; i++)
            {
                Grid[i] = new int[GridSize];
            }
        }
    }

}

