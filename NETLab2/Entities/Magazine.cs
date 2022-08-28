using System;

namespace NET_Lab2.Entities
{
    class Magazine
    {
        public int MagId { get; set; }
        public string Name { get; set; }
        // frequency of releases per month
        public double Freq { get; set; }
        // circulation
        public int Circ { get; set; }
        //established
        public DateTime Est{ get; set; }

        public override string ToString() => string.Format($"'{Name}' mag. " +
            $"est.{Est.ToString("d")}, {Freq} releases per month, {Circ}p. circulation");
    }
}
