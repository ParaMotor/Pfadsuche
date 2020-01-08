using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Hex
{
    public int xCoordinate { get; set; }
    public int yCoordinate { get; set; }
    public Boolean betretbar { get; set; }
    public Hex previous { get; set; }
    public List<Hex> nachbarn { get; set; }


    public List<Hex> getNachbarn()
    {
        return nachbarn;
    }
    public Boolean getBetretbar()
    {
        return betretbar;
    }
    public Hex getPrevious()
    {
        return previous;
    }
    public void setBetretbar(Boolean betr)
    {
        betretbar = betr;
    }
    public void setPrevious(Hex prev)
    {
        previous = prev;
    }
    public void addNachbar(Hex nachb)
    {
        nachbarn.Add(nachb);
    }
}

