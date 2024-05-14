namespace CherryPicking.Models
{
  
    public class CherryPickerModel
    {
        public int[][] Grid { get; set; } = new int[3][];
        public int MaxCherries { get; set; }
        public string SelectedOption { get; set; } = string.Empty;

        public CherryPickerModel()
        {
            for (int i = 0; i < 3; i++)
            {
                Grid[i] = new int[3];
            }
        }
    }
}

