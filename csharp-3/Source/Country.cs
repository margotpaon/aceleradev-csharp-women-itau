using System;

namespace Codenation.Challenge
{
    public class Country
    {        
        public State[] Top10StatesByArea()
        {
            State[] estados = new State[10];

            estados[0] = new State("Amazonas", "AM");
            estados[1] = new State("Pará", "PA");
            estados[2] = new State("Mato Grosso", "MT");
            estados[3] = new State("Minas Gerais", "MG");
            estados[4] = new State("Bahia", "BA");
            estados[5] = new State("Mato Grosso do Sul", "MS");
            estados[6] = new State("Goiás", "GO");
            estados[7] = new State("Maranhão", "MA");
            estados[8] = new State("Rio Grande do Sul", "RS");
            estados[9] = new State("Tocantins", "TO");

            return estados;
        }
    }
}
