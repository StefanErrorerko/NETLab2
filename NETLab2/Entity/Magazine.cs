using System;

namespace NET_Lab2.Entity
{
    class Magazine
    {
        public int MagId { get; set; }
        public string Name { get; set; }
        // frequencz of release per month
        public double Freq { get; set; }
        // circulation
        public int Circ { get; set; }
        //established
        public DateTime Est{ get; set; }

        public override string ToString() => string.Format($"'{Name}' mag. est.{Est}, {Freq} releases per month, {Circ}p. circulation");
    }
}
