using SolrEngine;
using System.Collections.Generic;
using System.Linq;

namespace SignalMonitoring.API.Managers
{
	public class GroupManager
	{
		private const int UNIQUE_ANSWER_POINTS = 5;
		private const int NON_UNIQUE_ANSWER_POINTS = 3;

		public List<AnswerModel> RedAnswers { get; set; } = new List<AnswerModel>();
		public List<AnswerModel> BlueAnswers { get; set; } = new List<AnswerModel>();


		public bool AddAndAssignPoints(AnswerModel answer, TeamEnum team)
		{
			var firstTeam = RedAnswers;
			var secondTeam = BlueAnswers;
			if (team == TeamEnum.Blue)
			{
				firstTeam = BlueAnswers;
				secondTeam = RedAnswers;
			}

			if (!answer.IsCorrect)
			{
				answer.PointsAchieved = 0;
				firstTeam.Add(answer);
			}

			if(firstTeam.Any(x => x.Id == answer.Id))
			{
				return false;
			}

			var itemInSecondList = secondTeam.Find(x => x.Id == answer.Id);

			if (itemInSecondList != null)
			{
				answer.PointsAchieved = NON_UNIQUE_ANSWER_POINTS;
				itemInSecondList.PointsAchieved = NON_UNIQUE_ANSWER_POINTS;
			}	
			else
			{
				answer.PointsAchieved = UNIQUE_ANSWER_POINTS;
			}

			firstTeam.Add(answer);
			return true;
		}	
	}
}
