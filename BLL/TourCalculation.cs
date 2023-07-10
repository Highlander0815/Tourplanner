using BLL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourplannerModel;

namespace BLL
{
    public class TourCalculation
    {
        public TourCalculation() 
        {
        }
        public void CalculatePopularity(TourModel tour)
        {
            if (tour.TourLogs != null)
                tour.Popularity = tour.TourLogs.Count;
            else
                tour.Popularity = 0;
        }
        public TimeSpan GetTotalTimeAverage(TourModel tour)
        {
            if (tour.TourLogs != null) //if no TourLogs are created for the current Tour the TotalTimeChildfriendliness will be estimated as bad
            {
                if (tour.TourLogs.Count >= 1)
                {
                    //calculate value of total time
                    TimeSpan totalTime = TimeSpan.Zero;
                    foreach (TourLogModel tourLog in tour.TourLogs)
                    {
                        totalTime += tourLog.TotalTime;
                    }
                    TimeSpan totalTimeAverage = new TimeSpan(((totalTime.Ticks) / tour.TourLogs.Count));
                    return totalTimeAverage;
                }
            }
            return new TimeSpan(0, 0, 0, 0);
        }
        public double GetAverageRating(TourModel tour)
        {
            double averageRating = 0.00;
            if(tour.TourLogs != null) 
            {
                foreach (TourLogModel tourLog in tour.TourLogs)
                {
                    averageRating += tourLog.Rating;
                }
                averageRating = averageRating / tour.TourLogs.Count;
            }
            return averageRating;
            
        }

        public void CalculateChildFriendliness(TourModel tour)
        {
            try
            {
                int totalTimeChildfriendliness = CalculateTotalTimeFriendliness(tour);
                int distanceChildfriendliness = CalculateDistanceFriendliness(tour);
                int difficultyChildfriendliness = CalculateDifficultyFriendliness(tour);

                tour.ChildFriendliness = totalTimeChildfriendliness + distanceChildfriendliness + difficultyChildfriendliness;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int CalculateDifficultyFriendliness(TourModel tour)
        {
            int difficultySum = 0; //if no TourLogs are created for the current Tour the DifficultyChildfriendliness will be estimated as beginner
            if(tour.TourLogs != null)
            {
                if (tour.TourLogs.Count >= 1)
                {
                    // calculate average of Difficulty
                    foreach (TourLogModel tourLog in tour.TourLogs)
                    {
                        difficultySum += Convert.ToInt32(tourLog.Difficulty);
                    }
                    double difficultyAverage = difficultySum / tour.TourLogs.Count; //muss noch gerundet werden weil zb 3+4 /2 = 3.5

                    return (int)Math.Round(difficultyAverage);
                }
            }
           
            return difficultySum;
        }

        private int CalculateDistanceFriendliness(TourModel tour)
        {
            //calculate value of Distance
            int distanceChildfriendliness = 5;
            try
            {
                if (tour.TourDistance == null)
                {
                    throw new ValueIsNullException();
                }
                if (tour.TourDistance < 4)
                {
                    distanceChildfriendliness = 0;
                }
                else if (tour.TourDistance < 6)
                {
                    distanceChildfriendliness = 1;
                }
                else if (tour.TourDistance < 7)
                {
                    distanceChildfriendliness = 2;
                }
                else if (tour.TourDistance < 8)
                {
                    distanceChildfriendliness = 3;
                }
                else if (tour.TourDistance < 9)
                {
                    distanceChildfriendliness = 4;
                }
            }
            catch (ValueIsNullException ex)
            {
                throw ex;
            }

            return distanceChildfriendliness;
        }
       
        private int CalculateTotalTimeFriendliness(TourModel tour)
        {
            int totalTimeChildfriendliness = 5;
            TimeSpan totalTimeAverage = GetTotalTimeAverage((TourModel)tour);  

            if (totalTimeAverage < new TimeSpan(1, 0, 0))
            {
                totalTimeChildfriendliness = 0;
            }
            else if (totalTimeAverage < new TimeSpan(2, 0, 0))
            {
                totalTimeChildfriendliness = 1;
            }
            else if (totalTimeAverage < new TimeSpan(2, 30, 0))
            {
                totalTimeChildfriendliness = 2;
            }
            else if (totalTimeAverage < new TimeSpan(3, 0, 0))
            {
                totalTimeChildfriendliness = 3;
            }
            else if (totalTimeAverage < new TimeSpan(3, 30, 0))
            {
                totalTimeChildfriendliness = 4;
            }

            return totalTimeChildfriendliness;
        }

       
    }
}
