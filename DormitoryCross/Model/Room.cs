using Microsoft.Maui.Graphics.Text;

namespace DormitoryCross.Model
{
    public class Room
    {
        public string Number { get; set; }
        public Color Color { get; set; }

        public Room (string number, Color color)
        {
            Number = number;
            Color = color;
        }
    }
}
