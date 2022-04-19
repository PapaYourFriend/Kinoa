namespace Kinoa.ViewModel.Models
{
    public class SeatModel
    {
        public int Row { get; set; }
        public int SeatInRow { get; set; }
        public SeatModel(int row, int seatInRow)
        {
            Row = row;
            SeatInRow = seatInRow;
        }
    }
}
