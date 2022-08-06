

namespace BlackJackHero.Assets.Code.Utils
{ 
    public class Names
    {
        private static readonly string[] _names = new string[]
    {
        "Anna        ","Eva         ","Maria       ","Karin       ","Kristina    ","Sara        ","Lena        ","Emma        ",
        "Kerstin     ","Ingrid      ","Marie       ","Malin       ","Jenny       ","Annika      ","Hanna       ","Linda       ",
        "Birgitta    ","Susanne     ","Elin        ","Monica      ","Inger       ","Sofia       ","Johanna     ","Carina      ",
        "Elisabeth   ","Ulla        ","Julia       ","Katarina    ","Linnéa      ","Emelie      ","Ida         ","Helena      ",
        "Camilla     ","Åsa         ","Anette      ","Sandra      ","Gunilla     ","Anita       ","Marianne    ","Margareta   ",
        "Anneli      ","Amanda      ","Maja        ","Ann         ","Therese     ","Josefin     ","Cecilia     ","Jessica     ",
        "Helen       ","Lisa        ","Caroline    ","Matilda     ","Barbro      ","Frida       ","Ulrika      ","Elsa        ",
        "Siv         ","Alice       ","Madeleine   ","Rebecca     ","Klara       ","Ebba        ","Sofie       ","Gun         ",
        "Agneta      ","Berit       ","Isabelle    ","Lina        ","Pia         ","Wilma       ","Ella        ","Yvonne      ",
        "Louise      ","Ellen       ","Britt       ","Astrid      ","Mona        ","Moa         ","Nathalie    ","Erika       ",
        "Alexandra   ","Emilia      ","Viktoria    ","Ann-Christin","Olivia      ","Alva        ","Agnes       ","Felicia     ",
        "Ann-Marie   ","Sonja       ","Britt-Marie ","Pernilla    ","Gunnel      ","Lovisa      ","Charlotte   ","Linn        ",
        "Lisbeth     ","Nina        ","Rut         ","Mikaela     ","Lars        ","Mikael      ","Anders      ","Johan       ",
        "Erik        ","Per         ","Karl        ","Peter       ","Thomas      ","Jan         ","Daniel      ","Fredrik     ",
        "Hans        ","Andreas     ","Stefan      ","Mohamed     ","Mats        ","Marcus      ","Mattias     ","Magnus      ",
        "Bengt       ","Jonas       ","Oskar       ","Alexander   ","Niklas      ","Martin      ","Bo          ","Nils        ",
        "Patrik      ","Viktor      ","Björn       ","Leif        ","David       ","Sven        ","Henrik      ","Filip       ",
        "Joakim      ","Christer    ","Ulf         ","Emil        ","Simon       ","Christoffer ","Anton       ","Gustav      ",
        "Robert      ","Tommy       ","Kjell       ","Christian   ","William     ","Lucas       ","Rickard     ","Håkan       ",
        "Göran       ","Rolf        ","Adam        ","Lennart     ","Jakob       ","Jonathan    ","Robin       ","Sebastian   ",
        "Tobias      ","Stig        ","Elias       ","John        ","Axel        ","Linus       ","Kent        ","Oliver      ",
        "Roger       ","Hugo        ","Isak        ","Claes       ","Jesper      ","Albin       ","Jörgen      ","Rasmus      ",
        "Ludvig      ","Jimmy       ","Max         ","Ali         ","Kenneth     ","Gunnar      ","Joel        ","Dennis      ",
        "Johnny      ","Josef       ","Olof        ","Åke         ","Olle        ","Kurt        ","Liam        ","Pontus      ",
        "Leo         ","Kevin       ","Samuel      ","Torbjörn    ","Felix       ","Edvin       ","Gabriel     ","Arvid       ",
    };
        public string[] All { get; }
        public string GetRandomName()
        {
            return All[UnityEngine.Random.Range(0, _names.Length - 1)];
        }
    }
}
