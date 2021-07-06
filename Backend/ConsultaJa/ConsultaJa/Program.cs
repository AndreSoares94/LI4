using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultaJaDB;

namespace ConsultaJa
{
    public class ConsultaJAT
    {   

        public static void work(ConsultaJaModel cjm, Conta c)
        {
            String s;
            while(!(s = Console.ReadLine()).Equals("logout"))
            {
                String[] list = s.Split(' ');
                switch (list[0])
                {
                    case "getClass":
                        if (c is Medico)
                        {
                            double classif = ((Medico)c).getClassificacao();
                            StringBuilder sb = new StringBuilder();
                            sb.Append("Classificação média atribuida: ");
                            if (classif == -1)
                                sb.Append("Sem classificações");
                            else
                                sb.Append(classif);
                            Console.WriteLine(sb.ToString());
                        }
                        break;

                    case "classificar":
                        if (c is Paciente)
                        {
                            try
                            {
                                cjm.avaliarMedico(list[1], Int32.Parse(list[2]));
                            }
                            catch(MailNaoRegistado exc)
                            {
                                Console.WriteLine(exc.Message);
                            }
                        }
                        break;
                }
            }
            Console.WriteLine("Logout efetuado co sucesso. Adeus, " + c.getNome());
        }
        /*
        static void Main(string[] args)
        {
            ConsultaJaModel cjm = new ConsultaJaModel();
            String message;
            while(!(message = Console.ReadLine()).Equals("exit"))
            {
                String[] list = message.Split(' ');
                switch (list[0])
                {
                    case "m_regista":
                        String s = cjm.novoMedico(list[1], list[2], list[3], null, new DateTime(1983,4,7), "MoradaDele", "nifdele");
                        Console.WriteLine("Id de médico atribuido: " + s);
                        break;

                    case "p_regista":
                        String s2 = cjm.novoPaciente(list[1], list[2], list[3], null, new DateTime(1983, 4, 7), "MoradaDele", "nifdele");
                        Console.WriteLine("Id de paciente atribuido: " + s2);
                        break;

                    case "login":
                        try
                        {
                            Conta c = cjm.login(list[1], list[2], list[3]);
                            Console.WriteLine("Login efetuado com sucesso, welcome " + list[2]);
                            work(cjm, c);
                        }
                        catch(MailNaoRegistado exc){
                            Console.WriteLine(exc.Message);
                        }
                        catch(PasswordErrada exc)
                        {
                            Console.WriteLine(exc.Message);
                        }
                        break;

                    default: 
                        break;
                }
            }
        }*/
    }
}
