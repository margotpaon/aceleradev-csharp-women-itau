using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class SubmissionService : ISubmissionService
    {
        private CodenationContext _context;
        public SubmissionService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Submission> FindByChallengeIdAndAccelerationId(int challengeId, int accelerationId)
        {
            var listSubmission = _context.Candidates.Where(x => x.AccelerationId.Equals(accelerationId))
                                                    .Select(x => x.User)
                                                    .SelectMany(x => x.Submissions)
                                                    .Where(x => x.ChallengeId.Equals(challengeId))
                                                    .Distinct()
                                                    .ToList();

            return listSubmission; //lista de submiss�es a partir do id do desafio e do id da acelera��o
        }

        public decimal FindHigherScoreByChallengeId(int challengeId)
        {
            return _context.Submissions.Where(x => x.ChallengeId.Equals(challengeId)).Max(x => x.Score);
        }

        public Submission Save(Submission submission)
        {
            // ignorar change tracker
            var existe = _context.Submissions.Find(submission.UserId, submission.ChallengeId);

            //Se não encontrar UserId ou ChallengeId adicione uma nova Submissão
            if (existe == null)
                _context.Submissions.Add(submission);
            //Caso contrário atualize o Score    
            else
                existe.Score = submission.Score;

            _context.SaveChanges();

            return submission;
        }
    }
}
