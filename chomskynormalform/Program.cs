using System.Transactions;
using chomskynormalform;

public class Program
{
    static void Main(string[] args)
    {
        ReadGrammar readGrammar = new ReadGrammar();
        readGrammar.ReadGrammarAll();
        RemovingEmpty removingEmpty = new RemovingEmpty();
        removingEmpty.RemovingAllEmpty();
        RemovingRenamings removingRenamings = new RemovingRenamings();
        removingRenamings.RemovingAllRenamings();
        RemovingUnreachable removingUnreachable = new RemovingUnreachable();
        removingUnreachable.RemovingAllUnreachable();
        RemovingNonproductive removingNonproductive = new RemovingNonproductive();
        removingNonproductive.RemovingAllNonproductive();
        ChomskyEnd chomskyEnd = new ChomskyEnd();
        chomskyEnd.DeleteSame();
        chomskyEnd.ChomskyTransform();
        

    }
}

