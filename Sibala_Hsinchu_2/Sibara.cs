﻿using System.Collections.Generic;
using System.Linq;

namespace Sibala_Hsinchu_2
{
    public class Sibara : ISibara
    {
        private List<int> _nums;

        public Sibara(int n1, int n2, int n3, int n4)
        {
            _nums = new List<int> { n1, n2, n3, n4 }.OrderByDescending(x => x).ToList();
            Compute();
        }

        public int Points { get; protected set; }
        public int MaxPoint { get; protected set; }

        public SibaraStatus.StatusEnum Status { get; protected set; }

        public string Output { get; protected set; }

        protected virtual void Compute()
        {
            if (IsSameColor())
            {
                this.Points = _nums.Sum() / 2;
                this.Status = SibaraStatus.StatusEnum.SameColor;
                this.MaxPoint = _nums.First();
                this.Output = "same color";
                return;
            }

            if (_nums.Distinct().Count() == 4)
            {
                Points = 0;
                Status = SibaraStatus.StatusEnum.NoPoint;
                this.MaxPoint = _nums.First();
            }
            else if (_nums.Distinct().Count() == 2)
            {
                var count = _nums.GroupBy(item => item).Select(item => item.Count()).Max();
                if (count == 3)
                {
                    Points = 0;
                    Status = SibaraStatus.StatusEnum.NoPoint;
                }
                else
                {
                    Points = _nums.Take(2).Sum();
                    Status = SibaraStatus.StatusEnum.Point;
                }

                this.MaxPoint = _nums.Max();
            }
            else
            {
                Points = _nums.GroupBy(x => x).Where(x => x.Count() == 1).Sum(x => x.Key);
                Status = SibaraStatus.StatusEnum.Point;
                this.MaxPoint = _nums.GroupBy(x => x).Where(x => x.Count() == 1).Max(x => x.Key);
            }

            SetSibaraResult();
        }

        private bool IsSameColor()
        {
            return _nums.Distinct().Count() == 1;
        }

        private void SetSibaraResult()
        {
            if (Status == SibaraStatus.StatusEnum.SameColor)
                this.Output = "same color";
            else if (Status == SibaraStatus.StatusEnum.NoPoint)
                this.Output = "no points";
            else if (Status == SibaraStatus.StatusEnum.Point)
            {
                if (Points == 12)
                    this.Output = "sibala";
                else if (Points == 3)
                {
                    this.Output = "BG";
                }
                else
                    this.Output = $"{Points} point";
            }
        }
    }
}