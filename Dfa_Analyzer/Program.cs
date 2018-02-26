using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfa_Analyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            DfaAnalyser f = new DfaAnalyser();

            string s = "a,b";
            Console.WriteLine("Enter alphabet set of language...");
            s = Console.ReadLine();

            f.buildSet(s);
            int count = 0;
            Console.WriteLine("Enter no of states...");
            count = int.Parse(Console.ReadLine());

            f.setStates(count);

            f.constructAutomata();
            while (true)
            {
                Console.Write("\n\n\n\n\t\t\tEnter a string:");
                string checkInput = Console.ReadLine();
                string v = f.CheckAutomata(checkInput);
                Console.WriteLine("\t\t\t"+v);
            }

            



        }
    }
    //==========================
    public class path
    {
        string key;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        int wayToHome;

        public int WayToHome
        {
            get { return wayToHome; }
            set { wayToHome = value; }
        }
    }
    //==========================
    class State
    {
        int id;

        public State()
        {
            homeWay = new List<path>();
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        bool isFinal;

        public bool IsFinal
        {
            get { return isFinal; }
            set { isFinal = value; }
        }

        List<path> homeWay;

        public List<path> HomeWay
        {
            get { return homeWay; }
            set { homeWay = value; }
        }

        public void addPath(path p)
        {
            homeWay.Add(p);
        }

        public int getPath(char skey)
        {
            int stateKey=0;
            foreach (path p in homeWay)
            {
                if (p.Key.Contains(skey))
                {
                    stateKey = p.WayToHome;
                    break;
                }

            }
            //var i = homeWay.First(x => x.Key.Equals(skey));
            //stateKey = i.WayToHome;

            return stateKey;
        }

        
    }
    //==========================
    public class DfaAnalyser
    {
       List<State> fa;
       State currentState;
        string[] alphabetSet;
        int stateCount;
        public DfaAnalyser()
        {
            fa = new List<State>();
        }
        public string CheckAutomata(string checkInput)
        {
           
            currentState = fa[0];
           
            if(CheckInputString(checkInput))
            {
                char[] checkIt = checkInput.ToCharArray();
                foreach (char s in checkIt)
                {
                    int nextState=currentState.getPath(s);
                    //Console.WriteLine("path"+nextState);
                    var p = from f in fa
                                   where f.Id==nextState
                                   select f;
                    foreach (var n in p)
                        currentState = n;

                    //Console.WriteLine(currentState.Id + " " + currentState.IsFinal);
                }

                if (currentState.IsFinal)
                {
                    
                    return "Accepted";
                }
                else
                {
                    return "rejected";
                }
            }

            else{

               return "Input string dose not belong to language";
                
            }

        }

        public void setStates(int count)
        {
            this.stateCount = count;
            for (int i = 1; i <= count; i++)
            {
                State s = new State();
                s.Id = i;
                fa.Add(s);
            }

            this.setFinal();

            //foreach (State q in fa)
            //{
            //    Console.WriteLine("id:" + q.Id);
            //    if(q.IsFinal)
            //        Console.WriteLine("Final");
            //    else
            //        Console.WriteLine("Ordinary");
            //}
        }

        public void setFinal()
        {
            foreach (State p in fa)
            {
                Console.Clear();
                Console.Write("\n\n\nn\nPress (y/Y) if state " + p.Id + " is final");

                string c = Console.ReadLine();
                if (c.Contains('y') || c.Contains('Y'))
                {
                   // Console.WriteLine("Finall maker");
                    p.IsFinal = true;
                }
                else
                    p.IsFinal = false;
            }
        }

        public void addNewState()
        {
            stateCount = +1;
            State s = new State();
            s.Id = this.stateCount;
        }

        public void constructAutomata()
        {
            foreach (State p in fa)
            {
                foreach (string way in alphabetSet)
                {
                    Console.Clear();
                    Console.Write("\n\n\n\nEnter path for " + way + " from state " + p.Id);
                    int state = int.Parse(Console.ReadLine());
                    while (true)
                    {
                        if (state > fa.Count || state < 1)
                        {
                            Console.WriteLine("Invalid state!Enter state again");
                            state = int.Parse(Console.ReadLine());
                        }
                        else
                            break;

                    }
                    path f = new path();

                    f.Key = way;
                    f.WayToHome = state;
                    p.addPath(f);

                }
            }
        }


        public bool CheckInputString(string input)
        {
            char[] arr = input.ToCharArray();
            bool flag = true;
            foreach (char f in arr)
            {
                
                foreach (string p in alphabetSet)
                {
                   
                    if (!p.Contains(f))
                    {
                        flag = false;

                    }
                    else
                    {
                        
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                    return false;
            }

            return flag;
        }

        public void buildSet(string s)
        {
            this.alphabetSet = s.Split(',');
            
        }
    }


}
