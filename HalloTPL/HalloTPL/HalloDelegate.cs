using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloTPL
{
    delegate void EinfacherDelegate();
    delegate void DelegateMitPara(string txt);
    delegate long CalcDelegate(int x, int y);

    class HalloDelegate
    {

        public DelegateMitPara deleMitPara;

        public HalloDelegate()
        {
            EinfacherDelegate meinDele = EinfacheMethode;
            Action meinDeleAlsAction = EinfacheMethode;

            Action meinDeleAlsActionAno = delegate () { Console.WriteLine("Hallo"); };
            Action meinDeleAlsActionAno2 = () => { Console.WriteLine("Hallo"); };
            Action meinDeleAlsActionAno3 = () => Console.WriteLine("Hallo");

            deleMitPara = MethodeMitPara;
            Action<string> deleMitParaAlsAction = MethodeMitPara;
            DelegateMitPara deleMitParaAno = (string txt) => { Console.WriteLine(txt); };
            DelegateMitPara deleMitParaAno2 = (txt) => Console.WriteLine(txt);
            DelegateMitPara deleMitParaAno3 = x => Console.WriteLine(x);

            CalcDelegate calcDele = Multi;
            Func<int, int, long> calcFunc = Sum;
            Func<int, int, long> calcFuncAno = (int a, int b) => { return a + b; };
            Func<int, int, long> calcFuncAno2 = (a, b) => { return a + b; };
            Func<int, int, long> calcFuncAno3 = (a, b) => a + b;

            Predicate<string> immerBool = ImmerBool;

            List<string> texte = new List<string>();
            texte.Where(x => x.StartsWith("b"));
            texte.Where(Filter);
        }

        private bool Filter(string arg)
        {
            if (arg.StartsWith("b"))
                return true;
            else
                return false;
        }

        private bool ImmerBool(string obj)
        {
            throw new NotImplementedException();
        }

        private long Multi(int x, int y)
        {
            return x * y;
        }

        private long Sum(int a, int b)
        {
            return a + b;
        }

        void MethodeMitPara(string msg)
        {
            Console.WriteLine(msg);
        }

        void EinfacheMethode()
        {
            Console.WriteLine("Hallo");
        }
    }
}
