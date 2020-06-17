using System;
using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Exceptions;

namespace Codenation.Challenge
{
    public class SoccerTeamsManager : IManageSoccerTeams
    {
        //lista com chave e valor -> chave long / valor Team
        private Dictionary<long, Team> teams;
        //lista com chave e valor -> chave long / valor Player
        private Dictionary<long, Player> players;
        public SoccerTeamsManager()
        {
            //ao instanciar a classe Team e Player são instanciados
            teams = new Dictionary<long, Team>();
            players = new Dictionary<long, Player>();
        }

        private Team GetTeam(long teamId)
        {
            Team teamRetorno;
            //sintaxe: tenta achar a chave "tif" (Try(entrada)-GetValue(saída-retorno)), 
            //caso não encontre retorna (out) o valor (valor) que pode ser utilizado em seguida dentro do if
            //quando não achar o Id do time lançamos uma exceção do Tipo TeamNotFoundException
            if (!teams.TryGetValue(teamId, out teamRetorno))
                throw new TeamNotFoundException();
            return teamRetorno;
        }

        private Player GetPlayer(long playerId)
        {
            Player player;
            //sintaxe: tenta achar a chave "tif" (Try(entrada)-GetValue(saída-retorno)), 
            //caso não encontre retorna (out) o valor (valor) que pode ser utilizado em seguida dentro do if
            //quando não achar o Id do Developer lançamos uma exceção do Tipo PlayerNotFoundException
            if (!players.TryGetValue(playerId, out player))
                throw new PlayerNotFoundException();
            return player;
        }

        public void AddTeam(long id, string name, DateTime createDate, string mainShirtColor, string secondaryShirtColor)
        {
            // ContainsKey pode ser usado para testar(verificar se já existe) chaves antes de inserir-las
            // Caso não encontre queremos lançar uma exceção do tipo UniqueIdentifierException
            if (teams.ContainsKey(id))
                throw new UniqueIdentifierException();

            var team = new Team()
            {
                Id = id,
                Name = name,
                CreateDate = createDate,
                MainShirtColor = mainShirtColor,
                SecondaryShirtColor = secondaryShirtColor
            };

            //add elemento na lista
            teams.Add(id, team);
        }

        public void AddPlayer(long id, long teamId, string name, DateTime birthDate, int skillLevel, decimal salary)
        {
            // ContainsKey pode ser usado para testar(verificar se já existe) chaves antes de inserir-las
            // Caso não encontre queremos lançar uma exceção do tipo UniqueIdentifierException
            if (players.ContainsKey(id))
                throw new UniqueIdentifierException();

            if (!teams.ContainsKey(teamId))
                throw new TeamNotFoundException();

            var team = GetTeam(teamId);

            var player = new Player()
            {
                Id = id,
                TeamId = team.Id,
                Name = name,
                BirthDate = birthDate,
                SkillLevel = skillLevel,
                Salary = salary
            };

            //add elemento na lista
            players.Add(id, player);
        }

        public void SetCaptain(long playerId)
        {
            Player player = GetPlayer(playerId);

            teams[player.TeamId].CaptainId = playerId;
        }

        public long GetTeamCaptain(long teamId)
        {
            Team team = GetTeam(teamId);
            if (!teams.ContainsKey(team.Id))
                throw new TeamNotFoundException();

            if (!team.CaptainId.HasValue)
                throw new CaptainNotFoundException();

            return team.CaptainId.Value;

        }

        public string GetPlayerName(long playerId)
        {
            Player player;
            if (!players.TryGetValue(playerId, out player))
                throw new PlayerNotFoundException();

            return player.Name;
        }

        public string GetTeamName(long teamId)
        {
            Team team;
            if (!teams.TryGetValue(teamId, out team))
                throw new TeamNotFoundException();
           
            return team.Name;
        }

        private IEnumerable<Player> GetPlayersOnTeam(long teamId)
        {
            //Para recuperar valores isolados, utiliza-se a propriedade Values
            //a lambda é uma mandeira sucinta de declarar uma função, sendo os parametros da função antes do =>, e o conteúdo da função depois.
            return players.Values.Where(x => x.TeamId == teamId);
        }

        public List<long> GetTeamPlayers(long teamId)
        {
            Team team;
            if (!teams.TryGetValue(teamId, out team))
                throw new TeamNotFoundException();
            return GetPlayersOnTeam(teamId).Select(x => x.Id).OrderBy(x => x).ToList();
        }

        public long GetBestTeamPlayer(long teamId)
        {
            Team team;

            if (!teams.TryGetValue(teamId, out team))
                throw new TeamNotFoundException();

            int highSkillLevel = players.Values.Where(x => x.TeamId.Equals(teamId)).Max(x => x.SkillLevel);
            var highSkillLevelRetorno = players.Values.Where(x => x.TeamId.Equals(teamId)).Where(x => x.SkillLevel.Equals(highSkillLevel)).Min(x => x.Id);
            return highSkillLevelRetorno;
        }

        public long GetOlderTeamPlayer(long teamId)
        {
            Team team;
            if (!teams.TryGetValue(teamId, out team))
                throw new TeamNotFoundException();

            DateTime youngPlayer = players.Values.Where(x => x.TeamId.Equals(teamId)).Min(x => x.BirthDate);
            var youngPlayerRetorno = players.Values.Where(x => x.TeamId.Equals(teamId)).Where(x => x.BirthDate.Equals(youngPlayer)).Min(x => x.Id);


            return youngPlayerRetorno;

        }

        public List<long> GetTeams()
        {
            List<long> getTeams = new List<long>();
            foreach (Team team in teams.Values)
                getTeams.Add(team.Id);

            getTeams.Sort();
            return getTeams;

        }

        public long GetHigherSalaryPlayer(long teamId)
        {
            Team team;
            if (!teams.TryGetValue(teamId, out team))
                throw new TeamNotFoundException();

            decimal highSalary = players.Values.Where(x => x.TeamId.Equals(teamId)).Max(x => x.Salary);
            var highSalaryRetorno = players.Values.Where(x => x.TeamId.Equals(teamId)).Where(x => x.Salary.Equals(highSalary)).Min(x => x.Id);

            return highSalaryRetorno;
        }

        public decimal GetPlayerSalary(long playerId)
        {
            Player player;
            if (!players.TryGetValue(playerId, out player))
                throw new PlayerNotFoundException();

            return player.Salary;
        }

        public List<long> GetTopPlayers(int top)
        {
            List<long> getTopPlayers = new List<long>();
            var topPlayers = players.Values.OrderByDescending(x => x.SkillLevel).Take(top);

            foreach (Player player in topPlayers)
                getTopPlayers.Add(player.Id);

            return getTopPlayers;

        }

        public string GetVisitorShirtColor(long teamId, long visitorTeamId)
        {
            Team homeTeam = teams[teamId];
            Team visitorTeam = teams[visitorTeamId];

            if (!teams.ContainsKey(teamId) || !teams.ContainsKey(visitorTeamId))
            {
                throw new TeamNotFoundException();
            }

            if (homeTeam.MainShirtColor.Equals(visitorTeam.MainShirtColor))
                return visitorTeam.SecondaryShirtColor;

            return visitorTeam.MainShirtColor;
        }

    }
}