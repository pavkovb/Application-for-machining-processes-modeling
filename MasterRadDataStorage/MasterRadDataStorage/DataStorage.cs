using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ConnectionToDB;
using MaterijalO;
using DodatakZaObradu;
using UkupanDodatakO;
using VrstaObrade;
using TipObr;
using OblikPl;
using KvalitetObradjenePovrsine;
using PripremakZaObradu;
using PrecnikPripremkaObrade;
using ReznaPl;
using RezimiO;
using KvalitetObradeReznePl;
using FilterDT;
using PreporucenRezimObrade;
using DrzacAlataNS;
using MasinaAlatka;
using System.Drawing;
using PrepZaMat;
using PrepZaPostojanostAlata;

namespace MasterRadDataStorage
{
    public abstract class DataStorageBasic
    {
        #region Fields

        protected DataTable table;
        protected int numOfRows;

        #endregion

        #region Methods

        protected void fillTable(string sqlCmd)
        {
            table = ConnToDB.GetTable(sqlCmd);
        }

        #endregion

        #region Constructor

        public DataStorageBasic(string sqlCmd)
        {
            fillTable(sqlCmd);
            numOfRows = table.Rows.Count;
        }

        #endregion
    }

    public abstract class DataStorage : DataStorageBasic
    {
        #region Fields

        protected Dictionary<string, object> dict = new Dictionary<string, object>();

        #endregion

        #region Properties

        protected List<string> listOfDictElements = new List<string>();
        public IList<string> ListOfDictElements
        {
            get { return listOfDictElements.AsReadOnly(); }
        }

        #endregion

        #region Methods

        protected void fillList()
        {
            listOfDictElements = dict.Keys.ToList();
        }
        protected abstract void fillDictionary();
        public abstract void ChooseElement(string elementName);

        #endregion

        #region Constructor

        public DataStorage(string sqlCmd)
           : base(sqlCmd) { }

        #endregion
    }

    public abstract class DataStorageWithFilter : DataStorage
    {
        #region Fields

        protected DataTable filtriranaTabela;
        protected int numOfFilterDtRows;

        #endregion

        #region Constructor

        public DataStorageWithFilter(string sqlCmd)
            : base(sqlCmd) { }

        #endregion

        #region Methods

        protected abstract bool filterCondition(DataRow dRow);


        protected void filterBaseTable()
        {
            if (filtriranaTabela != null)
            {
                filtriranaTabela.Clear();
            }
            TableFilter filter = new TableFilter();
            filter.OnRowFilter = filterCondition;
            filter.TableForFilter = table;
            filtriranaTabela = filter.FilterTable();
            numOfFilterDtRows = filtriranaTabela.Rows.Count;
        }

        #endregion

    }

    public class MaterijaliStorage : DataStorage
    {
        #region Fields

        public delegate void FillListDelegate();
        private string oznakaIzabranogMaterijala = "";

        #endregion

        #region Properties

        private static MaterijalObratka izabraniMaterijal;

        public static MaterijalObratka IzabraniMaterijal
        {
            get { return izabraniMaterijal; }
        }

        private static FillListDelegate fillListDelMaterijali;

        public static FillListDelegate FillListDelMaterijali
        {
            get { return fillListDelMaterijali; }
            set { fillListDelMaterijali = value; }
        }


        #endregion

        #region Enums

        enum MatTabCol { IndexCMC, IndexMaterial, IndexHardness, IndexKc1, IndexVrstaMat }

        #endregion

        #region Constructor

        public MaterijaliStorage()
            : base("SELECT CMC,Materijal,HB,Kc1,VrstaMaterijala FROM[View Materijali]")
        {
            fillDictionary();
            fillList();
        }

        #endregion

        #region Methods

        protected override void fillDictionary()
        {
            string oznakaMaterijala = "";

            for (int rowIndex = 0; rowIndex < numOfRows; rowIndex++)
            {
                MaterijalObratka materijal = new MaterijalObratka();
                materijal.Cmc = (double)table.Rows[rowIndex][(int)MatTabCol.IndexCMC];
                materijal.Materijal = (string)table.Rows[rowIndex][(int)MatTabCol.IndexMaterial];
                materijal.HB = (double)table.Rows[rowIndex][(int)MatTabCol.IndexHardness];
                materijal.SpecificnaSilaR = (double)table.Rows[rowIndex][(int)MatTabCol.IndexKc1];
                materijal.VrstaMaterijala = ((string)(table.Rows[rowIndex][(int)MatTabCol.IndexVrstaMat]))[0];
                oznakaMaterijala = materijal.Materijal;
                dict.Add(oznakaMaterijala, materijal);
            }
        }

        public override void ChooseElement(string elementName)
        {
            if (oznakaIzabranogMaterijala != elementName)
            {
                oznakaIzabranogMaterijala = elementName;
                object materijal;
                if (dict.TryGetValue(elementName, out materijal))
                {
                    izabraniMaterijal = materijal as MaterijalObratka;
                    if (fillListDelMaterijali != null)
                    {
                        fillListDelMaterijali();
                    }
                }
            }
        }

        #endregion
    }

    public class PreporukeZaPostojanostAlataStorage : DataStorageBasic
    {
        #region Properties

        private List<PreporukeZaPostojanostAlata> listaPreporukaZaPostojanost = new List<PreporukeZaPostojanostAlata>();

        public List<PreporukeZaPostojanostAlata> ListaPreporukaZaPostojanost
        {
            get { return listaPreporukaZaPostojanost; }
            set { listaPreporukaZaPostojanost = value; }
        }

        #endregion

        #region Enums

        enum PrepPostojanostTabCol { IndexOfPostojanostAlata, IndexOfKorekcioniFaktor }

        #endregion

        #region Constructor

        public PreporukeZaPostojanostAlataStorage()
            : base("SELECT PostojanostAlata,KorekcioniFaktor FROM [Preporuke za postojanost alata]")
        {
            fillListPreporukaZaPostojanost();
        }

        #endregion

        #region Methods

        private void fillListPreporukaZaPostojanost()
        {
            for (int rowIndex = 0; rowIndex < numOfRows; rowIndex++)
            {
                PreporukeZaPostojanostAlata preporuka = new PreporukeZaPostojanostAlata();
                preporuka.PostojanostAlata = (double)table.Rows[rowIndex][(int)PrepPostojanostTabCol.IndexOfPostojanostAlata];
                preporuka.KorekcioniFaktor = (double)table.Rows[rowIndex][(int)PrepPostojanostTabCol.IndexOfKorekcioniFaktor];
                listaPreporukaZaPostojanost.Add(preporuka);
            }
        }

        #endregion
    }

    public class VrstaObradeStorage : DataStorage
    {
        #region Fields

        public delegate void FillListVrstaObrDelegate();
        private string oznakaIzabraneVrsteObrade = "";

        #endregion

        #region Properties

        private static VrstaObr izabranaVrstaObrade;

        public static VrstaObr IzabranaVrstaObrade
        {
            get { return izabranaVrstaObrade; }
        }

        private static FillListVrstaObrDelegate fillListDel;

        public static FillListVrstaObrDelegate FillListDel
        {
            get { return fillListDel; }
            set { fillListDel = value; }
        }

        #endregion

        #region Enums

        enum VrstaObrTabCol { IndexVrstaObr }

        #endregion

        #region Constructor

        public VrstaObradeStorage()
            : base("SELECT VrstaObrade FROM [Vrste obrada]")
        {
            fillDictionary();
            fillList();
        }

        #endregion

        #region Methods

        protected override void fillDictionary()
        {
            string oznakaVrsteObrade = "";

            for (int rowIndex = 0; rowIndex < numOfRows; rowIndex++)
            {
                VrstaObr vrstaObrade = new VrstaObr();
                vrstaObrade.VrstaObrade = table.Rows[rowIndex][(int)VrstaObrTabCol.IndexVrstaObr].ToString();
                oznakaVrsteObrade = vrstaObrade.VrstaObrade;
                dict.Add(oznakaVrsteObrade, vrstaObrade);
            }
        }

        public override void ChooseElement(string elementName)
        {
            if (oznakaIzabraneVrsteObrade != elementName)
            {
                oznakaIzabraneVrsteObrade = elementName;
                object vrstaObrade;
                if (dict.TryGetValue(elementName, out vrstaObrade))
                {
                    izabranaVrstaObrade = vrstaObrade as VrstaObr;
                    if (fillListDel != null)
                    {
                        fillListDel();
                    }
                }
            }
        }

        #endregion
    }

    public class TipObradeStorage : DataStorage
    {
        #region Fields

        private Dictionary<string, Dictionary<string, TipObrade>> vrstaTipObradeDict = new Dictionary<string, Dictionary<string, TipObrade>>();
        public delegate void FillDictTipObrDelegate();
        private string oznakaIzabranogTipaObrade = "";
        string izabranaVrstaObrade = "";

        #endregion

        #region Properties

        private static FillDictTipObrDelegate fillListDel;

        public static FillDictTipObrDelegate FillListDel
        {
            get { return fillListDel; }
            set { fillListDel = value; }
        }

        private static TipObrade izabraniTipObrade;

        public static TipObrade IzabraniTipObrade
        {
            get { return izabraniTipObrade; }
        }

        #endregion

        #region Enums

        enum TipObrTabCol { IndexVrstaObrade, IndexTipObrade, IndexSlikaObrade }

        #endregion

        #region Constructor

        public TipObradeStorage()
            : base("SELECT VrstaObrade,TipObrade,SlikaObrade FROM [View Obrade] ORDER BY VrstaObrade")
        {
            fillVrstaTipObradeDict();
            VrstaObradeStorage.FillListDel = fillListTipObrade;
        }

        #endregion

        #region Methods

        protected override void fillDictionary()
        {
            if (dict != null)
            {
                dict.Clear();
            }
            if (izabranaVrstaObrade != VrstaObradeStorage.IzabranaVrstaObrade.VrstaObrade)
            {
                izabranaVrstaObrade = VrstaObradeStorage.IzabranaVrstaObrade.VrstaObrade;
                Dictionary<string, TipObrade> dictTipObrade;
                string nazivTipaObrade;
                TipObrade tipObrade;
                if (vrstaTipObradeDict.TryGetValue(izabranaVrstaObrade, out dictTipObrade))
                {
                    foreach (var tipObr in dictTipObrade)
                    {
                        nazivTipaObrade = tipObr.Value.TipObr;
                        tipObrade = tipObr.Value;
                        dict.Add(nazivTipaObrade, tipObrade as TipObrade);
                    }
                }
            }
        }

        private void fillVrstaTipObradeDict()
        {
            string vrstaObrade = "";
            string oznakaTipaObrade = "";
            Dictionary<string, TipObrade> dictValue;
            Dictionary<string, TipObrade> dictTipObrade;

            for (int rowIndex = 0; rowIndex < numOfRows; rowIndex++)
            {
                vrstaObrade = (string)table.Rows[rowIndex][(int)TipObrTabCol.IndexVrstaObrade];
                TipObrade tipObrade = new TipObrade();
                if (vrstaTipObradeDict.TryGetValue(vrstaObrade, out dictValue))
                {
                    dictTipObrade = dictValue;
                }
                else
                {
                    dictTipObrade = new Dictionary<string, TipObrade>();
                    vrstaTipObradeDict.Add(vrstaObrade, dictTipObrade);
                }
                tipObrade.TipObr = (string)table.Rows[rowIndex][(int)TipObrTabCol.IndexTipObrade];
                tipObrade.SlikaObr = (Bitmap)((new ImageConverter()).ConvertFrom((byte[])table.Rows[rowIndex][(int)TipObrTabCol.IndexSlikaObrade]));
                oznakaTipaObrade = tipObrade.TipObr;
                dictTipObrade.Add(oznakaTipaObrade, tipObrade);
            }
        }

        private void fillListTipObrade()
        {
            fillDictionary();
            fillList();
        }

        public override void ChooseElement(string elementName)
        {
            if (oznakaIzabranogTipaObrade != elementName)
            {
                oznakaIzabranogTipaObrade = elementName;
                object tipObrade;
                if (dict.TryGetValue(elementName, out tipObrade))
                {
                    izabraniTipObrade = tipObrade as TipObrade;
                    if (fillListDel != null)
                    {
                        fillListDel();
                    }
                }
            }
        }

        #endregion
    }

    public class KvalitetObradeStorage : DataStorage
    {
        #region Fileds

        public delegate void FillListDel();
        private string oznakaIzabranogKvaliteta = "";

        #endregion

        #region Properties

        private static KvalitetObrade izabraniKvalitet;

        public static KvalitetObrade IzabraniKvalitet
        {
            get { return izabraniKvalitet; }
        }

        private static FillListDel fillListDelKvalitetObrade;

        public static FillListDel FillListDelKvalitetObrade
        {
            get { return fillListDelKvalitetObrade; }
            set { fillListDelKvalitetObrade = value; }
        }


        #endregion

        #region Enums

        enum KvalitetiTabCol { IndexKlasaKvaliteta, IndexRaMax, IndexGradacijaObrade }

        #endregion

        #region Constructor

        public KvalitetObradeStorage()
            : base("SELECT KlasaKvaliteta,RaMax,GradacijaObrade FROM [Klase kvaliteta obradjene povrsine] ORDER BY RaMax")
        {
            fillDictionary();
            fillList();
        }

        #endregion

        #region Methods

        protected override void fillDictionary()
        {
            string oznakaKvaliteta = "";

            for (int rowIndex = 0; rowIndex < numOfRows; rowIndex++)
            {
                KvalitetObrade kvalitetObrade = new KvalitetObrade();
                kvalitetObrade.KlasaKvaliteta = (string)table.Rows[rowIndex][(int)KvalitetiTabCol.IndexKlasaKvaliteta];
                kvalitetObrade.RaMax = (double)table.Rows[rowIndex][(int)KvalitetiTabCol.IndexRaMax];
                kvalitetObrade.GradacijaObrade = (string)table.Rows[rowIndex][(int)KvalitetiTabCol.IndexGradacijaObrade];
                oznakaKvaliteta = kvalitetObrade.KlasaKvaliteta;
                dict.Add(oznakaKvaliteta, kvalitetObrade);
            }
        }

        public override void ChooseElement(string elementName)
        {
            if (oznakaIzabranogKvaliteta != elementName)
            {
                oznakaIzabranogKvaliteta = elementName;
                object kvalitet;
                if (dict.TryGetValue(elementName, out kvalitet))
                {
                    izabraniKvalitet = kvalitet as KvalitetObrade;
                    if (fillListDelKvalitetObrade != null)
                    {
                        fillListDelKvalitetObrade();
                    }
                }
            }
        }

        #endregion
    }

    public class DodaciStorage : DataStorageBasic
    {
        #region Properties

        private static UkupanDodatak izabraniDodatak = new UkupanDodatak();

        public static UkupanDodatak IzabraniDodatak
        {
            get { return izabraniDodatak; }
        }

        public delegate void FillListDelegate(double precnik, double duzina, UkupanDodatak dodatak);

        private static FillListDelegate chooseEDel;

        public static FillListDelegate ChooseElementDel
        {
            get { return chooseEDel; }
            set { chooseEDel = value; }
        }

        #endregion

        #region Enums

        enum DodaciTabCol { Dmin, Dmax, Lmin, Lmax, StrGrubo, StrFino, BrMin, BrMax };

        #endregion

        #region Constructor

        public DodaciStorage()
            : base("SELECT PrecnikMin,PrecnikMax,DuzinaMin,DuzinaMax,DodatakStrGrubo,DodatakStrFino,DodatakBrMin,DodatakBrMax FROM[View Dodaci za obradu]") { }

        #endregion

        #region Methods

        public void IzracunavanjeDodatka(double precnikObr, double duzinaObr)
        {
            int firstRowIndex = 0;
            int lastRowIndex = numOfRows - 1;
            double dMin = (double)table.Rows[firstRowIndex][(int)DodaciTabCol.Dmin];
            double dMax = (double)table.Rows[lastRowIndex][(int)DodaciTabCol.Dmax];
            double lMin = (double)table.Rows[firstRowIndex][(int)DodaciTabCol.Lmin];
            double lMax = (double)table.Rows[lastRowIndex][(int)DodaciTabCol.Lmax];

            if (precnikObr < dMin || precnikObr > dMax || duzinaObr < lMin || duzinaObr > lMax)
            {
                Exception parametriSuIzvanDozvoljenihGranica = new Exception("Nema podataka za postavljene kriterijume \n" +
                                                                             "Gornja granicna vrednost za prečnik: " + dMax + "\n" +
                                                                             "Donja granicna vrednost za prečnik: " + dMin + "\n" +
                                                                             "Gornja granicna vrednost za dužinu: " + lMax + "\n" +
                                                                             "Donja granicna vrednost za dužinu: " + dMin + "\n");
                throw parametriSuIzvanDozvoljenihGranica;
            }
            else
            {
                for (int rowIndex = 0; rowIndex < numOfRows; rowIndex++)
                {
                    dMin = (double)table.Rows[rowIndex][(int)DodaciTabCol.Dmin];
                    dMax = (double)table.Rows[rowIndex][(int)DodaciTabCol.Dmax];
                    lMin = (double)table.Rows[rowIndex][(int)DodaciTabCol.Lmin];
                    lMax = (double)table.Rows[rowIndex][(int)DodaciTabCol.Lmax];

                    if (dMin < precnikObr && precnikObr <= dMax && lMin < duzinaObr && duzinaObr <= lMax)
                    {
                        izabraniDodatak.DodatakZaGruguObradu.Dodatak = (double)table.Rows[rowIndex][(int)DodaciTabCol.StrGrubo];
                        izabraniDodatak.DodatakZaFinuObradu.Dodatak = (double)table.Rows[rowIndex][(int)DodaciTabCol.StrFino];
                        izabraniDodatak.DodatakZaBrusenjeMin.Dodatak = (double)table.Rows[rowIndex][(int)DodaciTabCol.BrMin];
                        izabraniDodatak.DodatakZaBrusenjeMax.Dodatak = (double)table.Rows[rowIndex][(int)DodaciTabCol.BrMax];
                        if (chooseEDel != null)
                        {
                            chooseEDel(precnikObr, duzinaObr, izabraniDodatak);
                        }
                        break;
                    }
                }
            }
        }

        #endregion
    }

    public class PripremciStorage : DataStorageBasic

    {
        #region Fields

        private static List<PrecnikPripremka> listaPrecnikaPripremaka = new List<PrecnikPripremka>();

        #endregion

        #region Properties

        private Pripremak izabraniPripremak = new Pripremak();

        public Pripremak IzabraniPripremak
        {
            get { return izabraniPripremak; }
            set { izabraniPripremak = value; }
        }

        #endregion

        #region Enums

        enum PripremciTabCol { IndexPrecnik }

        #endregion

        #region Constructor

        public PripremciStorage()
            : base("SELECT Precnik FROM [Precnici pripremaka]")
        {
            fillListPrecniciPripremaka();
            DodaciStorage.ChooseElementDel = odredjivanjePripremka;
        }

        #endregion

        #region Methods

        private void odredjivanjePripremka(double precnikObrade, double duzinaObrade, UkupanDodatak dodatak)
        {
            double minimalniPrecnik = precnikObrade + 2 * dodatak.VrednostUkupanogDodatakaMin;
            double duzina = duzinaObrade + dodatak.VrednostUkupanogDodatakaMin;
            double precnik = 0;
            for (int listIndex = 0; listIndex < listaPrecnikaPripremaka.Count; listIndex++)
            {
                precnik = listaPrecnikaPripremaka[listIndex].Precnik;
                if (precnik >= minimalniPrecnik)
                {
                    izabraniPripremak.PrecnikPripremka.Precnik = precnik;
                    izabraniPripremak.Duzina = duzina;
                    izabraniPripremak.Duzina = duzinaObrade;
                    break;
                }
            }
        }

        protected void fillListPrecniciPripremaka()
        {
            for (int rowIndex = 0; rowIndex < numOfRows; rowIndex++)
            {
                PrecnikPripremka precnikPripremka = new PrecnikPripremka();
                precnikPripremka.Precnik = (double)table.Rows[rowIndex][(int)PripremciTabCol.IndexPrecnik];
                listaPrecnikaPripremaka.Add(precnikPripremka);
            }
        }

        #endregion
    }

    public class ObliciPlocicaStorage : DataStorageBasic
    {
        #region Fields

        public static List<OblikPlocice> listaOblikaPlocica = new List<OblikPlocice>();

        #endregion

        #region Constructor

        public ObliciPlocicaStorage()
            : base("SELECT VrstaObrade, TipObrade, OblikPlocice FROM[View Oblici plocica]")
        {
            TipObradeStorage.FillListDel = fillListOblikPlocica;
        }

        #endregion

        #region Enums

        enum ObliciPlTabCol { IndexVrstaObrade, IndexTipObrade, IndexOblikPlocice }

        #endregion

        #region Methods

        protected void fillListOblikPlocica()
        {
            if (listaOblikaPlocica != null)
            {
                listaOblikaPlocica.Clear();
            }
            string vrstaObrade = VrstaObradeStorage.IzabranaVrstaObrade.VrstaObrade;
            string tipObrade = TipObradeStorage.IzabraniTipObrade.TipObr;
            string uslovVrsteO = "";
            string uslovTipO = "";

            for (int rowIndex = 0; rowIndex < numOfRows; rowIndex++)
            {
                uslovVrsteO = (string)table.Rows[rowIndex][(int)ObliciPlTabCol.IndexVrstaObrade];
                uslovTipO = (string)table.Rows[rowIndex][(int)ObliciPlTabCol.IndexTipObrade];
                if (vrstaObrade == uslovVrsteO && tipObrade == uslovTipO)
                {
                    OblikPlocice oblikPl = new OblikPlocice();
                    oblikPl.OblikPloc = ((string)table.Rows[rowIndex][(int)ObliciPlTabCol.IndexOblikPlocice])[0];
                    listaOblikaPlocica.Add(oblikPl);
                }
            }
        }

        #endregion
    }

    public class KvalitetiReznihPlocicaStorage : DataStorageBasic
    {
        #region Fields

        public static List<KvalitetObradeReznePlocice> listaKvalitetaReznihPlocica = new List<KvalitetObradeReznePlocice>();
        public delegate void KvalitetiPlDel();

        #endregion

        #region Properties

        private static KvalitetiPlDel fillDictKvalitetiPl;

        public static KvalitetiPlDel FillDictKvalitetiPl
        {
            get { return fillDictKvalitetiPl; }
            set { fillDictKvalitetiPl = value; }
        }

        #endregion

        #region Enums

        enum KvalitetiPlocicaTabCol { IndexVrstaMaterijala, IndexKvalitetObrade, IndexGradacijaObrade }

        #endregion

        #region Constructor

        public KvalitetiReznihPlocicaStorage()
            : base("SELECT VrstaMaterijala,KvalitetObrade,GradacijaObrade FROM [View Kvaliteti plocica]")
        {
            MaterijaliStorage.FillListDelMaterijali = fillListKvalitetiPlocica;
            KvalitetObradeStorage.FillListDelKvalitetObrade = fillListKvalitetiPlocica;
        }

        #endregion

        #region Methods

        public void fillListKvalitetiPlocica()
        {
            if (listaKvalitetaReznihPlocica != null)
            {
                listaKvalitetaReznihPlocica.Clear();
            }
            char uslovVrsteMaterijala = '\0';
            string uslovGradacijeObrade = "";
            char vrstaMaterijala;
            string gradacijaObrade;
            if (MaterijaliStorage.IzabraniMaterijal != null && KvalitetObradeStorage.IzabraniKvalitet != null)
            {
                uslovVrsteMaterijala = MaterijaliStorage.IzabraniMaterijal.VrstaMaterijala;
                uslovGradacijeObrade = KvalitetObradeStorage.IzabraniKvalitet.GradacijaObrade;
                for (int rowIndex = 0; rowIndex < numOfRows; rowIndex++)
                {
                    vrstaMaterijala = ((string)table.Rows[rowIndex][(int)KvalitetiPlocicaTabCol.IndexVrstaMaterijala])[0];
                    gradacijaObrade = (string)table.Rows[rowIndex][(int)KvalitetiPlocicaTabCol.IndexGradacijaObrade];
                    if (uslovVrsteMaterijala == vrstaMaterijala && uslovGradacijeObrade == gradacijaObrade)
                    {
                        KvalitetObradeReznePlocice kvalitetPlocice = new KvalitetObradeReznePlocice();
                        kvalitetPlocice.OznakaKvalitetaPlocice = ((string)table.Rows[rowIndex][(int)KvalitetiPlocicaTabCol.IndexKvalitetObrade]).TrimEnd();
                        listaKvalitetaReznihPlocica.Add(kvalitetPlocice);
                    }
                }
                if (fillDictKvalitetiPl != null)
                {
                    fillDictKvalitetiPl();
                }
            }
        }

        #endregion

    }

    public class PreporukeISOPStorage : DataStorageWithFilter
    {
        #region Fields

        public delegate void PreporukeDelegate();
        string oznakaIzabranogKvaliteta = "";

        #endregion

        #region Properties

        private static PreporukeDelegate fillDict;

        public static PreporukeDelegate FillDict
        {
            get { return fillDict; }
            set { fillDict = value; }
        }

        private static List<string> listaUslovaKvaliteta;

        public static List<string> ListaUslovaKvaliteta
        {
            set { listaUslovaKvaliteta = value; }
        }


        private static PreporukeZaMaterijal izabranePreporuke = new PreporukeZaMaterijal();

        public static PreporukeZaMaterijal IzabranePreporuke
        {
            get { return izabranePreporuke; }
        }

        #endregion

        #region Enums

        enum PreporukeTabCol { IndexOfCMC, IndexOfKvalitet, IndexOfV1, IndexOfS1, IndexOfV2, IndexOfS2, IndexOfV3, IndexOfS3 };

        #endregion

        #region Constructor

        public PreporukeISOPStorage()
            : base("SELECT CMC,Kvalitet,V1,S1,V2,S2,V3,S3 FROM [Preporuke za materijal]")
        {
            PrepRezimiStorage.FillDictKvaliteti = fillPreporuke;
            PrepRezimiStorage.OdabirPreporuke = ChooseElement;
        }

        private void fillPreporuke()
        {
            filterBaseTable();
            fillDictionary();
            fillList();
        }

        #endregion

        #region Methods

        public override void ChooseElement(string elementName)
        {
            if (oznakaIzabranogKvaliteta != elementName)
            {
                oznakaIzabranogKvaliteta = elementName;
                object preporuke;
                if (dict.TryGetValue(elementName, out preporuke))
                {
                    izabranePreporuke = preporuke as PreporukeZaMaterijal;
                    if (fillDict != null)
                    {
                        fillDict();
                    }
                }
            }
        }

        protected override void fillDictionary()
        {
            if (dict != null)
            {
                dict.Clear();
            }
            string oznakaKvaliteta = "";
            PreporukeZaMaterijal preporuke;

            for (int rowIndex = 0; rowIndex < numOfFilterDtRows; rowIndex++)
            {
                preporuke = new PreporukeZaMaterijal();
                preporuke.OznakaKvaliteta = (string)filtriranaTabela.Rows[rowIndex][(int)PreporukeTabCol.IndexOfKvalitet];
                preporuke.V1 = (double)filtriranaTabela.Rows[rowIndex][(int)PreporukeTabCol.IndexOfV1];
                preporuke.V2 = (double)filtriranaTabela.Rows[rowIndex][(int)PreporukeTabCol.IndexOfV2];
                preporuke.V3 = (double)filtriranaTabela.Rows[rowIndex][(int)PreporukeTabCol.IndexOfV3];
                preporuke.S1 = (double)filtriranaTabela.Rows[rowIndex][(int)PreporukeTabCol.IndexOfS1];
                preporuke.S2 = (double)filtriranaTabela.Rows[rowIndex][(int)PreporukeTabCol.IndexOfS2];
                preporuke.S3 = (double)filtriranaTabela.Rows[rowIndex][(int)PreporukeTabCol.IndexOfS3];
                oznakaKvaliteta = preporuke.OznakaKvaliteta;
                dict.Add(oznakaKvaliteta, preporuke);
            }
        }

        protected override bool filterCondition(DataRow dRow)
        {
            double uslovCMC = MaterijaliStorage.IzabraniMaterijal.Cmc;
            double cmc = (double)dRow[(int)PreporukeTabCol.IndexOfCMC];
            string uslovKvalitet = (string)dRow[(int)PreporukeTabCol.IndexOfKvalitet];
            string kvalitet = "";
            int numOfListElements = listaUslovaKvaliteta.Count;

            for (int listIndex = 0; listIndex < numOfListElements; listIndex++)
            {
                kvalitet = listaUslovaKvaliteta[listIndex];
                if (uslovKvalitet == kvalitet && uslovCMC == cmc)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

    }

    public class PrepRezimiStorage : DataStorageWithFilter
    {
        #region Fields

        private Dictionary<string, Dictionary<string, PreporucenRezim>> prepRezimiDict = new Dictionary<string, Dictionary<string, PreporucenRezim>>();
        private string oznakaIzabranogKvaliteta = "";
        private string oznakaIzabranePlocice = "";
        public delegate void PrepRezimiDelegate();
        public delegate void PrepRezPreporukeDelegate(string oznaka);

        #endregion

        #region Properties

        private static PrepRezimiDelegate fillDictDrzaci;

        public static PrepRezimiDelegate FillDictDrzaci
        {
            get { return fillDictDrzaci; }
            set { fillDictDrzaci = value; }
        }

        private static PrepRezimiDelegate fillDictKvaliteti;

        public static PrepRezimiDelegate FillDictKvaliteti
        {
            get { return fillDictKvaliteti; }
            set { fillDictKvaliteti = value; }
        }

        private static PrepRezPreporukeDelegate odabirPreporuke;

        public static PrepRezPreporukeDelegate OdabirPreporuke
        {
            get { return odabirPreporuke; }
            set { odabirPreporuke = value; }
        }

        private static PreporucenRezim izabraniRezimiO;

        public static PreporucenRezim IzabraniRezimiO
        {
            get { return izabraniRezimiO; }
        }

        protected List<string> listaOznakaKvaliteta = new List<string>();

        public IList<string> ListaOznakaKvaliteta
        {
            get { return listaOznakaKvaliteta.AsReadOnly(); }
        }

        protected List<string> listaFiltriranihOznakaPlocica = new List<string>();

        public IList<string> ListaFiltriranihOznakaPlocica
        {
            get { return listaFiltriranihOznakaPlocica.AsReadOnly(); }
        }

        #endregion

        #region Enums

        enum PrepRezimiTabCol
        {
            IndexKvalitet, IndexOznakaPlocice, IndexOblikPlocice, IndexLedjniUgao, IndexTolerancija, IndexTipPlocice, IndexDuzinaRezneIvice,
            IndexDebljina, IndexRadijus, IndexKvalitetObrade, IndexAPreporuceno, IndexAMin, IndexAMax, IndexSPreporuceno, IndexSMin, indexSMax
        }

        #endregion

        #region Constructor

        public PrepRezimiStorage()
            : base("SELECT Kvalitet, OznakaPlocice, OblikPlocice, LedjniUgao, Tolerancija, TipPlocice, DuzinaRezneIvice, Debljina, " +
                   "Radijus, KvalitetObrade, APreporuceno, AMin, AMax, SPreporuceno, SMin, SMax FROM [View Preporuceni rezimi]")
        {
            KvalitetiReznihPlocicaStorage.FillDictKvalitetiPl = OdabirKvaliteta;
        }

        #endregion

        #region Methods

        protected override void fillDictionary()
        {
            if (prepRezimiDict != null)
            {
                prepRezimiDict.Clear();
            }
            string oznakaKvaliteta = "";
            string oznakaPlocice = "";
            Dictionary<string, PreporucenRezim> dictValue;
            Dictionary<string, PreporucenRezim> dictRezimiObrade;

            for (int rowIndex = 0; rowIndex < numOfFilterDtRows; rowIndex++)
            {
                oznakaKvaliteta = ((string)filtriranaTabela.Rows[rowIndex][(int)PrepRezimiTabCol.IndexKvalitet]).TrimEnd();
                PreporucenRezim prepRezimObr = new PreporucenRezim();
                if (prepRezimiDict.TryGetValue(oznakaKvaliteta, out dictValue))
                {
                    dictRezimiObrade = dictValue;
                }
                else
                {
                    dictRezimiObrade = new Dictionary<string, PreporucenRezim>();
                    prepRezimiDict.Add(oznakaKvaliteta, dictRezimiObrade);
                }
                prepRezimObr.ReznaPl.OznakaPlocice = (string)filtriranaTabela.Rows[rowIndex][(int)PrepRezimiTabCol.IndexOznakaPlocice];
                prepRezimObr.ReznaPl.Oblik.OblikPloc = ((string)filtriranaTabela.Rows[rowIndex][(int)PrepRezimiTabCol.IndexOblikPlocice])[0];
                prepRezimObr.ReznaPl.LedjniUgao = ((string)filtriranaTabela.Rows[rowIndex][(int)PrepRezimiTabCol.IndexLedjniUgao])[0];
                prepRezimObr.ReznaPl.Tolerancija = ((string)filtriranaTabela.Rows[rowIndex][(int)PrepRezimiTabCol.IndexTolerancija])[0];
                prepRezimObr.ReznaPl.TipPlocice = ((string)filtriranaTabela.Rows[rowIndex][(int)PrepRezimiTabCol.IndexTipPlocice])[0];
                prepRezimObr.ReznaPl.DuzinaRezneIvice = (double)filtriranaTabela.Rows[rowIndex][(int)PrepRezimiTabCol.IndexDuzinaRezneIvice];
                prepRezimObr.ReznaPl.Debljina = (double)filtriranaTabela.Rows[rowIndex][(int)PrepRezimiTabCol.IndexDebljina];
                prepRezimObr.ReznaPl.Radijus = (double)filtriranaTabela.Rows[rowIndex][(int)PrepRezimiTabCol.IndexRadijus];
                prepRezimObr.ReznaPl.KvalitetObrade.OznakaKvalitetaPlocice = (string)filtriranaTabela.Rows[rowIndex][(int)PrepRezimiTabCol.IndexKvalitetObrade];
                prepRezimObr.RezimObr.APrep = (double)filtriranaTabela.Rows[rowIndex][(int)PrepRezimiTabCol.IndexAPreporuceno];
                prepRezimObr.RezimObr.AMin = (double)filtriranaTabela.Rows[rowIndex][(int)PrepRezimiTabCol.IndexAMin];
                prepRezimObr.RezimObr.AMax = (double)filtriranaTabela.Rows[rowIndex][(int)PrepRezimiTabCol.IndexAMax];
                prepRezimObr.RezimObr.SPrep = (double)filtriranaTabela.Rows[rowIndex][(int)PrepRezimiTabCol.IndexSPreporuceno];
                prepRezimObr.RezimObr.SMin = (double)filtriranaTabela.Rows[rowIndex][(int)PrepRezimiTabCol.IndexSMin];
                prepRezimObr.RezimObr.SMax = (double)filtriranaTabela.Rows[rowIndex][(int)PrepRezimiTabCol.indexSMax];
                oznakaPlocice = prepRezimObr.ReznaPl.OznakaPlocice;
                dictRezimiObrade.Add(oznakaPlocice, prepRezimObr);
            }
        }
        private void fillDictReznePlocice(string izabraniKvalitetObrade)
        {
            if (oznakaIzabranogKvaliteta != izabraniKvalitetObrade)
            {
                if (dict != null)
                {
                    dict.Clear();
                }
                oznakaIzabranogKvaliteta = izabraniKvalitetObrade;
                Dictionary<string, PreporucenRezim> dictPrepRezimi;
                string oznakaPlocice;
                PreporucenRezim preporuceniRezimi;
                if (prepRezimiDict.TryGetValue(izabraniKvalitetObrade, out dictPrepRezimi))
                {
                    foreach (var preporuceniRezim in dictPrepRezimi)
                    {
                        oznakaPlocice = preporuceniRezim.Value.ReznaPl.OznakaPlocice;
                        preporuceniRezimi = preporuceniRezim.Value;
                        dict.Add(oznakaPlocice, preporuceniRezimi);
                    }
                }
            }
        }

        public override void ChooseElement(string elementName)
        {
            if (oznakaIzabranePlocice != elementName)
            {
                oznakaIzabranePlocice = elementName;
                object prepRezimiObrade;
                if (dict.TryGetValue(elementName, out prepRezimiObrade))
                {
                    izabraniRezimiO = prepRezimiObrade as PreporucenRezim;
                    if (fillDictDrzaci != null)
                    {
                        fillDictDrzaci();
                    }
                }
            }
        }

        protected void fillListKvaliteti()
        {
            listaOznakaKvaliteta = prepRezimiDict.Keys.ToList();
        }

        private void OdabirKvaliteta()
        {
            filterBaseTable();
            fillDictionary();
            fillListKvaliteti();
            PreporukeISOPStorage.ListaUslovaKvaliteta = listaOznakaKvaliteta;
            if (fillDictKvaliteti != null)
            {
                fillDictKvaliteti();
            }
        }

        public void OdabirPlocice(string izabraniKvalitetObrade)
        {
            fillDictReznePlocice(izabraniKvalitetObrade);
            fillList();
            odabirPreporuke(izabraniKvalitetObrade);
        }

        protected override bool filterCondition(DataRow dRow)
        {
            List<KvalitetObradeReznePlocice> listaKvalitetaPlocica = KvalitetiReznihPlocicaStorage.listaKvalitetaReznihPlocica;
            int numOfListElements = listaKvalitetaPlocica.Count;
            string uslovKvalitetaPlocice = "";
            string vrednostKvaliteta = (string)dRow[(int)PrepRezimiTabCol.IndexKvalitetObrade];
            for (int listIndex = 0; listIndex < numOfListElements; listIndex++)
            {
                uslovKvalitetaPlocice = listaKvalitetaPlocica[listIndex].OznakaKvalitetaPlocice;
                if (uslovKvalitetaPlocice == vrednostKvaliteta)
                {
                    return true;
                }
            }
            return false;
        }

        public void FiltriranjePlocica(ReznaPlocica plocica)
        {
            if (dict != null)
            {
                foreach (var rezim in dict)
                {

                }
            }
        }

        #endregion
    }

    public class DrzaciAlataStorage : DataStorageWithFilter
    {
        #region Fields

        public static DrzacAlata izabraniDrzacAlata;
        private string oznakaIzabranogDrzaca = "";

        #endregion

        #region Properties

        #endregion

        #region Enums

        enum DrzaciAlataTabCol
        {
            IndexOznakaDrzaca, IndexPricvrscivanjePlocice, IndexOblikPlocice, IndexNapadniUgao, IndexLedjniUgao, IndexSmer, IndexVisinaDrzaca, IndexSirinaDrzaca,
            IndexDuzinaAlata, IndexIC, IndexF1, IndexH1, IndexL3, IndexGama, IndexLambda, IndexMomentPritezanja, IndexVrednostDuzine, IndexVrednostNapadnogUgla
        }

        #endregion

        #region Constructor

        public DrzaciAlataStorage()
            : base("SELECT OznakaDrzaca,PricvrscivanjePlocice,OblikPlocice,NapadniUgao,LedjniUgao,Smer,VisinaDrzaca,SirinaDrzaca," +
                  "DuzinaAlata,iC,f1,h1,l3,Gama,Lambda,MomentPritezanja,VrednostDuzine,VrednostNapadnogUgla FROM [View Drzaci alata]")
        {
            PrepRezimiStorage.FillDictDrzaci = metod;
        }

        #endregion

        #region Methods

        protected override void fillDictionary()
        {
            if (dict != null)
            {
                dict.Clear();
            }
            string oznakaDrzaca = "";
            DrzacAlata drzacAlata;
            for (int rowIndex = 0; rowIndex < numOfFilterDtRows; rowIndex++)
            {
                drzacAlata = new DrzacAlata();
                drzacAlata.OznakaDrzaca = (string)filtriranaTabela.Rows[rowIndex][(int)DrzaciAlataTabCol.IndexOznakaDrzaca];
                drzacAlata.PricvrscivanjePlocice = ((string)filtriranaTabela.Rows[rowIndex][(int)DrzaciAlataTabCol.IndexPricvrscivanjePlocice])[0];
                drzacAlata.Oblik.OblikPloc = ((string)filtriranaTabela.Rows[rowIndex][(int)DrzaciAlataTabCol.IndexOblikPlocice])[0];
                drzacAlata.NapadniUgao = ((string)filtriranaTabela.Rows[rowIndex][(int)DrzaciAlataTabCol.IndexNapadniUgao])[0];
                drzacAlata.LedjniUgao = ((string)filtriranaTabela.Rows[rowIndex][(int)DrzaciAlataTabCol.IndexLedjniUgao])[0];
                drzacAlata.Smer = ((string)filtriranaTabela.Rows[rowIndex][(int)DrzaciAlataTabCol.IndexSmer])[0];
                drzacAlata.VisinaDrzaca = (double)filtriranaTabela.Rows[rowIndex][(int)DrzaciAlataTabCol.IndexVisinaDrzaca];
                drzacAlata.SirinaDrzaca = (double)filtriranaTabela.Rows[rowIndex][(int)DrzaciAlataTabCol.IndexSirinaDrzaca];
                drzacAlata.DuzinaAlata = ((string)filtriranaTabela.Rows[rowIndex][(int)DrzaciAlataTabCol.IndexDuzinaAlata])[0];
                drzacAlata.IC = (double)filtriranaTabela.Rows[rowIndex][(int)DrzaciAlataTabCol.IndexIC];
                drzacAlata.F1 = (double)filtriranaTabela.Rows[rowIndex][(int)DrzaciAlataTabCol.IndexF1];
                drzacAlata.H1 = (double)filtriranaTabela.Rows[rowIndex][(int)DrzaciAlataTabCol.IndexH1];
                drzacAlata.L3 = (double)filtriranaTabela.Rows[rowIndex][(int)DrzaciAlataTabCol.IndexL3];
                drzacAlata.Gama = (double)filtriranaTabela.Rows[rowIndex][(int)DrzaciAlataTabCol.IndexGama];
                drzacAlata.Lambda = (double)filtriranaTabela.Rows[rowIndex][(int)DrzaciAlataTabCol.IndexLambda];
                drzacAlata.MomentPritezanja = (double)filtriranaTabela.Rows[rowIndex][(int)DrzaciAlataTabCol.IndexMomentPritezanja];
                drzacAlata.VrednostDuzineAlata = (double)filtriranaTabela.Rows[rowIndex][(int)DrzaciAlataTabCol.IndexVrednostDuzine];
                drzacAlata.VrednostNapadnogUgla = (double)filtriranaTabela.Rows[rowIndex][(int)DrzaciAlataTabCol.IndexVrednostNapadnogUgla];
                oznakaDrzaca = drzacAlata.OznakaDrzaca;
                dict.Add(oznakaDrzaca, drzacAlata);
            }
        }

        private void metod()
        {
            filterBaseTable();
            fillDictionary();
            fillList();
        }

        public override void ChooseElement(string elementName)
        {
            if (oznakaIzabranogDrzaca != elementName)
            {
                oznakaIzabranogDrzaca = elementName;
                object drzacAlata;
                if (dict.TryGetValue(elementName, out drzacAlata))
                {
                    izabraniDrzacAlata = drzacAlata as DrzacAlata;
                }
            }
        }

        protected override bool filterCondition(DataRow dRow)
        {
            char uslovOblika = PrepRezimiStorage.IzabraniRezimiO.ReznaPl.Oblik.OblikPloc;
            char vrednostOblikaPlocice = ((string)dRow[(int)DrzaciAlataTabCol.IndexOblikPlocice])[0];
            char uslovLedjnogUgla = PrepRezimiStorage.IzabraniRezimiO.ReznaPl.LedjniUgao;
            char vrednostLedjnogUgla = ((string)dRow[(int)DrzaciAlataTabCol.IndexLedjniUgao])[0];
            double uslovIC = PrepRezimiStorage.IzabraniRezimiO.ReznaPl.DuzinaRezneIvice;
            double vrednostIC = (double)dRow[(int)DrzaciAlataTabCol.IndexIC];
            return uslovOblika == vrednostOblikaPlocice && uslovLedjnogUgla == vrednostLedjnogUgla && uslovIC == vrednostIC;
        }

        #endregion

    }

    public class MasineDataStorage : DataStorageWithFilter
    {
        #region Fileds

        private string oznakaIzabraneMasine = "";
        double uslovSnageMasine = 0;
        double uslovPrecnika = 0;
        double uslovDuzine = 0;
        double uslovBrojaObrtaja = 0;

        #endregion

        #region Properties

        private static MasinaA izabranaMasina;

        public static MasinaA IzabranaMasina
        {
            get { return izabranaMasina; }
        }

        #endregion

        #region Enums

        enum MasineAlatkeTabCol
        {
            IndexMasina, IndexDMax, IndexDOprimalno, IndexLMax, IndexNMin, IndexNMax, IndexPm, IndexStepenIskoristenja
        }

        #endregion

        #region Constructor

        public MasineDataStorage()
            : base("SELECT Masina,DMax,DOprimalno,LMax,NMin,NMax,Pm,StepenIskoristenja FROM [Masine alatke]")
        {
        }

        #endregion

        #region Methods

        private bool filterMasina(DataRow dRow)
        {
            double pm = (double)dRow[(int)MasineAlatkeTabCol.IndexPm];
            double maxPrecnik = (double)dRow[(int)MasineAlatkeTabCol.IndexDMax];
            double maxDuzina = (double)dRow[(int)MasineAlatkeTabCol.IndexLMax];
            double maxBrojObrtaja = (double)dRow[(int)MasineAlatkeTabCol.IndexNMax];
            return uslovSnageMasine <= pm && uslovPrecnika <= maxPrecnik && uslovDuzine <= maxDuzina && uslovBrojaObrtaja <= maxBrojObrtaja;
        }

        public void PostavljanjeUslovaZaIzborMasine(double uslovSnageMasine, double uslovPrecnika, double uslovDuzine, double uslovBrojaObrtaja)
        {
            this.uslovSnageMasine = uslovSnageMasine;
            this.uslovPrecnika = uslovPrecnika;
            this.uslovDuzine = uslovDuzine;
            this.uslovBrojaObrtaja = uslovBrojaObrtaja;
            filterBaseTable();
            fillDictionary();
            fillList();
        }

        public override void ChooseElement(string elementName)
        {
            if (oznakaIzabraneMasine != elementName)
            {
                oznakaIzabraneMasine = elementName;
                object masina;
                if (dict.TryGetValue(elementName, out masina))
                {
                    izabranaMasina = masina as MasinaA;
                }
            }
        }

        protected override void fillDictionary()
        {
            if (dict!=null)
            {
                dict.Clear();
            }
            MasinaA masina;
            string oznakaMasine = "";
            for (int rowIndex = 0; rowIndex < numOfFilterDtRows; rowIndex++)
            {
                masina = new MasinaA();
                masina.NazivMasine = (string)filtriranaTabela.Rows[rowIndex][(int)MasineAlatkeTabCol.IndexMasina];
                masina.DMax = (double)filtriranaTabela.Rows[rowIndex][(int)MasineAlatkeTabCol.IndexDMax];
                masina.DOpt = (double)filtriranaTabela.Rows[rowIndex][(int)MasineAlatkeTabCol.IndexDOprimalno];
                masina.LMax = (double)filtriranaTabela.Rows[rowIndex][(int)MasineAlatkeTabCol.IndexLMax];
                masina.NMin = (double)filtriranaTabela.Rows[rowIndex][(int)MasineAlatkeTabCol.IndexNMin];
                masina.NMax = (double)filtriranaTabela.Rows[rowIndex][(int)MasineAlatkeTabCol.IndexNMax];
                masina.Pm = (double)filtriranaTabela.Rows[rowIndex][(int)MasineAlatkeTabCol.IndexPm];
                masina.StepenIskoristenja = (double)filtriranaTabela.Rows[rowIndex][(int)MasineAlatkeTabCol.IndexStepenIskoristenja];
                oznakaMasine = masina.NazivMasine;
                dict.Add(oznakaMasine, masina);
            }
        }

        protected override bool filterCondition(DataRow dRow)
        {
            double pm = (double)dRow[(int)MasineAlatkeTabCol.IndexPm];
            double maxPrecnik = (double)dRow[(int)MasineAlatkeTabCol.IndexDMax];
            double maxDuzina = (double)dRow[(int)MasineAlatkeTabCol.IndexLMax];
            double maxBrojObrtaja = (double)dRow[(int)MasineAlatkeTabCol.IndexNMax];
            return uslovSnageMasine <= pm && uslovPrecnika <= maxPrecnik && uslovDuzine <= maxDuzina && uslovBrojaObrtaja <= maxBrojObrtaja;
        }

        #endregion

    }

    public class GradacijeObradaStorage : DataStorageBasic
    {
        #region Properties

        private List<string> listaGradacija = new List<string>();

        public List<string> ListaGradacija
        {
            get { return listaGradacija; }
        }

        #endregion

        #region Enums

        enum GradacijeTabCol { IndexOfGradacijaObrade }

        #endregion

        #region Constructor

        public GradacijeObradaStorage()
            : base("SELECT GradacijaObrade FROM [Gradacije obrada] ORDER BY GradacijaObrade")
        {
            fillListGradacija();
        }

        #endregion

        #region Methods

        private void fillListGradacija()
        {
            string gradacija = "";
            for (int rowIndex = 0; rowIndex < numOfRows; rowIndex++)
            {
                gradacija = (string)table.Rows[rowIndex][(int)GradacijeTabCol.IndexOfGradacijaObrade];
                listaGradacija.Add(gradacija);
            }
        }
        #endregion
    }

    public class SlikeZaFormuStorage : DataStorageBasic
    {
        #region Properties

        private List<Image> listaSlikaZaFormu = new List<Image>();

        public List<Image> ListaSlikaZaFormu
        {
            get { return listaSlikaZaFormu; }
        }

        #endregion

        #region Enums

        enum SlikeZaFormuTabCol { IndexOfSlika }

        #endregion

        #region Constructor

        public SlikeZaFormuStorage()
            : base("SELECT Slika FROM [Slike za formu]")
        {
            fillListSlikaZaFormu();
        }

        #endregion

        #region Methods

        private void fillListSlikaZaFormu()
        {
            Image slika = null;
            for (int rowIndex = 0; rowIndex < numOfRows; rowIndex++)
            {
                slika = (Bitmap)((new ImageConverter()).ConvertFrom((byte[])table.Rows[rowIndex][(int)SlikeZaFormuTabCol.IndexOfSlika]));
                listaSlikaZaFormu.Add(slika);
            }
        }
        #endregion
    }
}
