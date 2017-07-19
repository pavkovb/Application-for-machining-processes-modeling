using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MasterRadDataStorage;
using ReznaPl;
using RezimiO;
using DrzacAlataNS;
using MasinaAlatka;
using ConnectionToDB;
using MaterijalO;
using NumInputCont;
using UkupanDodatakO;
using Intrpl;
using PrepZaMat;
using InterpBrzina;
using FilterReznihPl;
using GraphDraw;
using PrepZaPostojanostAlata;
using PointCoord;
using System.Windows.Forms.DataVisualization.Charting;
using IzracunatiRez;
using IzracunatiPrepRezimi;
using PrepRezim;
using RacunanjeGranicneVrednostiDubineRezanja;
using RacunanjeParametaraRez;

namespace MasterR
{
    public partial class Form1 : Form
    {
        #region Fileds

        public string connString;
        MaterijaliStorage materijaliSt;
        VrstaObradeStorage vrstaObradeSt;
        TipObradeStorage tipObradeSt;
        ObliciPlocicaStorage oblikPlociceSt;
        KvalitetObradeStorage kvalitetObradeSt;
        DodaciStorage dodaciSt;
        PripremciStorage pripremaciSt;
        KvalitetiReznihPlocicaStorage kvalitetiRPlSt;
        PrepRezimiStorage prepRezimiSt;
        DrzaciAlataStorage drzaciSt;
        MasineDataStorage masineSt;
        PreporukeISOPStorage preporukeISOPSt;
        GradacijeObradaStorage gradacijeObradeSt;
        SlikeZaFormuStorage slikeZaFormuSt;
        InterpolacijaBrzina interpolacijaBrzina;
        FilterReznihPlocica filterPlocica;
        List<ReznaPlocica> listaPlocica;
        GraphDrawing graphDrawBrzinaProizvodnost;
        GraphDrawing graphDrawPostojanostProizvodnost;
        GraphDrawing graphDrawBrzinaPostojanost;
        PreporukeZaPostojanostAlataStorage preporukeZaPostojanost;
        List<PointCoordinates> listaTacaka;
        RezimiObrade rezimi;
        ReznaPlocica plocica;
        IzracunatiRezimi izracunatiRezimi;
        IzracunatiPreporuceniRezimi izracunatiPrepRezimi;
        PreporuceniRezim preporuceniRezim;
        GranicnaVrednostDubneRezanja granicnaVrednostDubine;
        Chart izabraniChart;
        RacunanjeParametaraRezanja racunanjeParametaraRezanja;
        double granicnaVrednostPomaka;
        double granicnaVrednostBrzine;
        double diameter;

        #endregion

        #region Methods

        private void brisanjeIzabranogGrafika()
        {
            izabraniChart = null;
        }

        private void postavljanjeIzabranogGrafika(Chart chart)
        {
            izabraniChart = chart;
        }

        private void fillComboBox(ComboBox comboBox, IList<string> list)
        {
            int numOfListElements = list.Count;
            for (int elementIndex = 0; elementIndex < numOfListElements; elementIndex++)
            {
                comboBox.Items.Add(list[elementIndex]);
            }
        }

        private void fillComboBoxAndSetToDropDownList(ComboBox combobox, IList<string> list)
        {
            combobox.Items.Clear();
            fillComboBox(combobox, list);
            combobox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private double convertStringToDouble(string str)
        {
            double result;
            bool isDouble = double.TryParse(str, out result);
            return result;
        }

        private void IzaberiDodatke()
        {
            if (precnikObratkaNumericUpDown.Value >= 1 && duzinaObradeNumericUpDown.Value >= 1)
            {
                diameter = (double)precnikObratkaNumericUpDown.Value;
                double length = (double)duzinaObradeNumericUpDown.Value;
                bool fillD = true;
                try
                {
                    dodaciSt.IzracunavanjeDodatka(diameter, length);
                }
                catch (Exception ex)
                {
                    fillD = false;
                    MessageBox.Show(ex.Message);

                }
                if (fillD)
                {
                    fillDodaciPripremak();
                }
                else
                {
                    clearDodaci();
                }
            }
        }

        private void clearDodaci()
        {
            d1TextBox.Clear();
            d2TextBox.Clear();
            d3TextBox.Clear();
        }

        private void fillDodaciPripremak()
        {
            fillDodaci();
            double precnikPripremka = pripremaciSt.IzabraniPripremak.PrecnikPripremka.Precnik;
            precnikPripremkaTextBox.Text = precnikPripremka.ToString();
            granicnaVrednostDubine.PrecnikPripremka = precnikPripremka - diameter;
            racunanjeParametaraRezanja.Precnik = precnikPripremka;
            granicnaVrednostDubine.RacunanjeGranicneVrednostiDubineRezanja();
        }

        private void fillDodaci()
        {
            UkupanDodatak dodatak = DodaciStorage.IzabraniDodatak;
            granicnaVrednostDubine.Dodatak = dodatak;
            d1TextBox.Text = dodatak.DodatakZaGruguObradu.Dodatak.ToString();
            d2TextBox.Text = dodatak.DodatakZaFinuObradu.Dodatak.ToString();
            d3TextBox.Text = dodatak.DodatakZaBrusenjeMin.Dodatak.ToString();
        }

        private void fillCheckedListBox(CheckedListBox checkedListBox, IList<string> list)
        {
            for (int listIndex = 0; listIndex < list.Count; listIndex++)
            {
                checkedListBox.Items.Add(list[listIndex]);
            }
        }

        private void napuniListuPlocica()
        {
            listaPlocica.Clear();
            foreach (string item in reznePlociceCheckedListBox.Items)
            {
                prepRezimiSt.ChooseElement(item.ToString());
                ReznaPlocica plocica = PrepRezimiStorage.IzabraniRezimiO.ReznaPl;
                listaPlocica.Add(plocica);
            }
        }

        private void odlaganjeItemCheckEventa(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate { PopunjavanjeNaEventItemCheck(e, sender as CheckedListBox); });
        }

        public void PopunjavanjeNaEventItemCheck(ItemCheckEventArgs e, CheckedListBox cLBox)
        {
            filterPlocica.IzracunavanjeMogucihUslova();
            filtriranje(e, cLBox);
            filterPlocica.FiltriranjeListe();
            popunjavanjeCheckedListBoxova();
        }

        private delegate void PopunjavanjePropDelegate(CheckedListBox cLBox);

        private PopunjavanjePropDelegate popunjavanjeListeDel;

        private void filtriranje(ItemCheckEventArgs e, CheckedListBox cLBox)
        {
            if (cLBox.CheckedItems.Count > 0)
            {
                popunjavanjeListeDel(cLBox);
            }
            postaviUsloveFiltriranja();
        }

        private void popuniListuOblikaPlocica(CheckedListBox cLBox)
        {
            List<char> list = cLBoxToCharList(cLBox);
            if (list.Count > 0)
            {
                filterPlocica.ListaOblikaPlocica = list;
            }
        }

        private void popuniListuLedjnihUglova(CheckedListBox cLBox)
        {
            List<char> list = cLBoxToCharList(cLBox);
            if (list.Count > 0)
            {
                filterPlocica.ListaLedjnihUgaova = list;
            }
        }
        private void popuniListuTolerancija(CheckedListBox cLBox)
        {
            List<char> list = cLBoxToCharList(cLBox);
            if (list.Count > 0)
            {
                filterPlocica.ListaTolerancija = list;
            }
        }
        private void popuniListuTipovaPlocica(CheckedListBox cLBox)
        {
            List<char> list = cLBoxToCharList(cLBox);
            if (list.Count > 0)
            {
                filterPlocica.ListaTipovaPlocica = list;
            }
        }
        private void popuniListuDuzina(CheckedListBox cLBox)
        {
            List<double> list = cLBoxToDoubleList(cLBox);
            if (list.Count > 0)
            {
                filterPlocica.ListaDuzinaReznihIvica = list;
            }
        }
        private void popuniListuDebljina(CheckedListBox cLBox)
        {
            List<double> list = cLBoxToDoubleList(cLBox);
            if (list.Count > 0)
            {
                filterPlocica.ListaDebljina = list;
            }
        }
        private void popuniListuRadijusa(CheckedListBox cLBox)
        {
            List<double> list = cLBoxToDoubleList(cLBox);
            if (list.Count > 0)
            {
                filterPlocica.ListaRadijusa = list;
            }
        }
        private void popuniListuKvaliteta(CheckedListBox cLBox)
        {
            List<string> list = cLBoxToStringList(cLBox);
            if (list.Count > 0)
            {
                filterPlocica.ListaKvalitetaObrade = list;
            }
        }

        private List<char> cLBoxToCharList(CheckedListBox cLBox)
        {
            List<char> resultList = new List<char>();
            char outParameter = '\0';
            bool isChar = false;
            foreach (var item in cLBox.CheckedItems)
            {
                isChar = char.TryParse(item.ToString(), out outParameter);
                if (isChar)
                {
                    resultList.Add(outParameter);
                }
            }
            return resultList;
        }

        private List<double> cLBoxToDoubleList(CheckedListBox cLBox)
        {
            List<double> resultList = new List<double>();
            double outParameter = 0;
            bool isChar = false;
            foreach (var item in cLBox.CheckedItems)
            {
                isChar = double.TryParse(item.ToString(), out outParameter);
                if (isChar)
                {
                    resultList.Add(outParameter);
                }
            }
            return resultList;
        }

        private List<string> cLBoxToStringList(CheckedListBox cLBox)
        {
            List<string> resultList = new List<string>();
            foreach (var item in cLBox.CheckedItems)
            {
                resultList.Add(item.ToString());
            }
            return resultList;
        }

        private void popunjavanjeCLBoxa(CheckedListBox cLBox, List<string> list)
        {
            if (cLBox.SelectedItems.Count == 0)
            {
                cLBox.Items.Clear();
                foreach (var item in list)
                {
                    cLBox.Items.Add(item.ToString());
                }
            }
        }

        private void popunjavanjeCLBoxa(CheckedListBox cLBox, List<char> list)
        {
            if (cLBox.SelectedItems.Count == 0)
            {
                cLBox.Items.Clear();
                foreach (var item in list)
                {
                    cLBox.Items.Add(item.ToString());
                }
            }
        }
        private void popunjavanjeCLBoxa(CheckedListBox cLBox, List<double> list)
        {
            if (cLBox.SelectedItems.Count == 0)
            {
                cLBox.Items.Clear();
                foreach (var item in list)
                {
                    cLBox.Items.Add(item.ToString());
                }
            }
        }

        private void popunjavanjeCheckedListBoxova()
        {
            popunjavanjeCLBoxa(oblikPlociceCLBox, filterPlocica.ListaOblikaPlocica);
            popunjavanjeCLBoxa(ledjniUgaoCLBox, filterPlocica.ListaLedjnihUgaova);
            popunjavanjeCLBoxa(tolerancijaCLBox, filterPlocica.ListaTolerancija);
            popunjavanjeCLBoxa(tipPlociceCLBox, filterPlocica.ListaTipovaPlocica);
            popunjavanjeCLBoxa(duzinaRezneIviceCLBox, filterPlocica.ListaDuzinaReznihIvica);
            popunjavanjeCLBoxa(debljinaCLBox, filterPlocica.ListaDebljina);
            popunjavanjeCLBoxa(radijusCLBox, filterPlocica.ListaRadijusa);
            popunjavanjeCLBoxa(kvalitetiCLBox, filterPlocica.ListaKvalitetaObrade);

            reznePlociceCheckedListBox.Items.Clear();
            foreach (var item in FilterReznihPlocica.filtriranaListaPlocica)
            {
                reznePlociceCheckedListBox.Items.Add(item.OznakaPlocice);
            }
        }

        private void postaviUsloveFiltriranja()
        {
            popuniListuOblikaPlocica(oblikPlociceCLBox);
            popuniListuLedjnihUglova(ledjniUgaoCLBox);
            popuniListuTolerancija(tolerancijaCLBox);
            popuniListuTipovaPlocica(tipPlociceCLBox);
            popuniListuDuzina(duzinaRezneIviceCLBox);
            popuniListuDebljina(debljinaCLBox);
            popuniListuRadijusa(radijusCLBox);
            popuniListuKvaliteta(kvalitetiCLBox);
        }

        private void crtanjeGrafikaIPopunjavanje()
        {
            string oznakaPlocice = reznePlociceCheckedListBox.SelectedItem.ToString();
            prepRezimiSt.ChooseElement(oznakaPlocice);
            rezimi = PrepRezimiStorage.IzabraniRezimiO.RezimObr;
            plocica = PrepRezimiStorage.IzabraniRezimiO.ReznaPl;
            racunanjeBrzina(rezimi, plocica);
            racunanjeTacakaZaDijagramPostojanostProizvodnost();
            racunanjeTacakaZaDijagramBrzinaPostojanost();
        }

        private void uklanjanjeSerijaSaGrafika(string uslovUklanjanja, Chart chart)
        {
            string tagSerije = "";
            foreach (Series serija in chart.Series.Reverse())
            {
                tagSerije = serija.Tag.ToString();
                if (tagSerije == uslovUklanjanja)
                {
                    chart.Series.Remove(serija);
                }
            }
        }

        private double racunanjeGranicneVrednostiBrzine()
        {
            return Math.Round(Interpolation.NevilleInterpolator(izracunatiRezimi.Pomaci, izracunatiRezimi.Brzine, granicnaVrednostPomaka), 0, MidpointRounding.AwayFromZero);
        }

        private void racunanjeBrzina(RezimiObrade rezimi, ReznaPlocica plocica)
        {
            interpolacijaBrzina.Rezimi = rezimi;
            double vMax = Math.Round(interpolacijaBrzina.vMax, 0, MidpointRounding.AwayFromZero);
            double vPrep = Math.Round(interpolacijaBrzina.vPrep, 0, MidpointRounding.AwayFromZero);
            double vMin = Math.Round(interpolacijaBrzina.vMin, 0, MidpointRounding.AwayFromZero);

            double sMax = rezimi.SMax;
            double sPrep = rezimi.SPrep;
            double sMin = rezimi.SMin;
            double aMax = rezimi.AMax;
            double aPrep = rezimi.APrep;
            double aMin = rezimi.AMin;

            izracunatiPrepRezimi.MaxRezim.Brzina = vMax;
            izracunatiPrepRezimi.MaxRezim.Pomak = sMin;
            izracunatiPrepRezimi.MaxRezim.Dubina = aMin;
            izracunatiPrepRezimi.PrepRezim.Brzina = vPrep;
            izracunatiPrepRezimi.PrepRezim.Pomak = sPrep;
            izracunatiPrepRezimi.PrepRezim.Dubina = aPrep;
            izracunatiPrepRezimi.MinRezim.Brzina = vMin;
            izracunatiPrepRezimi.MinRezim.Pomak = sMax;
            izracunatiPrepRezimi.MinRezim.Dubina = aMax;

            List<double> brzine = new List<double> { vMin, vPrep, vMax };
            List<double> pomaci = new List<double> { sMax, sPrep, sMin };
            List<double> dubine = new List<double> { aMax, aPrep, aMin };

            izracunatiRezimi.Brzine = brzine;
            izracunatiRezimi.Pomaci = pomaci;
            izracunatiRezimi.Dubine = dubine;

            double brzina = 0;
            double pomak = 0;
            double dubina = 0;
            string oznakaSerije = "";

            for (int dubinaIndex = 0; dubinaIndex < izracunatiRezimi.Dubine.Count; dubinaIndex++)
            {
                for (int listIndex = 0; listIndex < izracunatiRezimi.Brzine.Count; listIndex++)
                {
                    PointCoordinates point = new PointCoordinates();
                    brzina = izracunatiRezimi.Brzine[listIndex];
                    pomak = pomaci[listIndex];
                    dubina = dubine[dubinaIndex];
                    point.XCoordinate = brzina;
                    point.YCoordinate = Math.Round(brzina * pomak * dubina, 1, MidpointRounding.AwayFromZero);
                    listaTacaka.Add(point);
                }
                oznakaSerije = plocica.OznakaPlocice + "\n" + "a = " + dubina + "   s = " + pomak;
                graphDrawBrzinaProizvodnost.DrawGraph(listaTacaka, oznakaSerije, plocica.OznakaPlocice, 0);
                listaTacaka.Clear();
                prikazSerija();
            }

            oznakaRPlTBox.Text = plocica.OznakaPlocice;
            oblikRPlTBox.Text = plocica.Oblik.OblikPloc.ToString();
            ledjniUgaoTBox.Text = plocica.LedjniUgao.ToString();
            tolerancijaTBox.Text = plocica.Tolerancija.ToString();
            tipPlTBox.Text = plocica.TipPlocice.ToString();
            duzinaRezneIviceTBox.Text = plocica.DuzinaRezneIvice.ToString();
            debljinaPlTBox.Text = plocica.Debljina.ToString();
            radijusPlTBox.Text = plocica.Radijus.ToString();
            kvalitetObradeTextBox.Text = plocica.KvalitetObrade.OznakaKvalitetaPlocice.ToString();

            vMaxTextBox.Text = vMax.ToString();
            vPrepTextbox.Text = vPrep.ToString();
            vMinTextBox.Text = vMin.ToString();
            sMaxTextBox.Text = rezimi.SMax.ToString();
            sPrepTextBox.Text = rezimi.SPrep.ToString();
            sMinTextBox.Text = rezimi.SMin.ToString();
            aMaxTextBox.Text = rezimi.AMax.ToString();
            aPrepTextBox.Text = rezimi.APrep.ToString();
            aMinTextBox.Text = rezimi.AMin.ToString();

            granicnaVrednostPomaka = racunanjeGranicneVrednostiPomaka(plocica.Radijus, KvalitetObradeStorage.IzabraniKvalitet.RaMax);
            izabraniPomakNumericUpDown.Maximum = (decimal)granicnaVrednostPomaka;
            izabraniPomakNumericUpDown.Value = (decimal)granicnaVrednostPomaka;
            pomakUFunkcijiOdRaTextBox.Text = granicnaVrednostPomaka.ToString();
            dubinaUzavisnostiOdDodatkaTBox.Text = granicnaVrednostDubine.GranicnaVrednostDubine.ToString();
            izabranaDubinaNumericUpDown.Value = (decimal)granicnaVrednostDubine.GranicnaVrednostDubine;
            granicnaVrednostBrzine = racunanjeGranicneVrednostiBrzine();
            izabranaBrzinaNumericUpDown.Value = (decimal)granicnaVrednostBrzine;
            brzinaUZavisnostiOdPostojanostiTextBox.Text = granicnaVrednostBrzine.ToString();
            racunanjeParametaraRezanja.Radijus = plocica.Radijus;
            unosBrzinePomakaIDubine();
            popunjavanjeParametaraRezanjaIPotrebnihUslova();
        }

        private void unosBrzinePomakaIDubine()
        {
            double brzina = (double)izabranaBrzinaNumericUpDown.Value;
            double pomak = (double)izabraniPomakNumericUpDown.Value;
            double dubina = (double)izabranaDubinaNumericUpDown.Value;
            if (brzina > 0 && pomak > 0 && dubina > 0)
            {
                racunanjeParametaraRezanja.Brzina = brzina;
                racunanjeParametaraRezanja.Pomak = pomak;
                racunanjeParametaraRezanja.Dubina = dubina;
            }
        }

        private void popunjavanjeParametaraRezanjaIPotrebnihUslova()
        {
            if (racunanjeParametaraRezanja.sviParametriSuUneti)
            {
                potrebnaSnagaMotoraTextBox.Text = racunanjeParametaraRezanja.PotrebnaSnagaMotora.ToString();
                potrebanBrojObrtajaTextBox.Text = racunanjeParametaraRezanja.BrojObrtaja.ToString();
                proizvodnostTextBox.Text = racunanjeParametaraRezanja.Proizvodnost.ToString();
                vitkostTextBox.Text = racunanjeParametaraRezanja.KoeficientVitkostiStrugotine.ToString();
                raTextBox.Text = racunanjeParametaraRezanja.SrednjaAritmetickaHrapavost.ToString();
                rtTextBox.Text = racunanjeParametaraRezanja.MaksimalnaHrapavost.ToString();
            }
        }


        private void prikazSerija()
        {
            prikazPrepurucenihSerija();
            prikazMaxSerija();
            prikazMinSerija();
            prikazLabelaZaSveGrafike();
        }

        private void prikazPrepurucenihSerija()
        {
            int indexPreporucenihSerija = 1;
            prikazSerija(indexPreporucenihSerija, preporuceneSerijeCheckBox.Checked, brzinaProizvodnostChart);
            prikazSerija(indexPreporucenihSerija, preporuceneSerijeCheckBox.Checked, postojanostProizvodnostChart);
            prikazSerija(indexPreporucenihSerija, preporuceneSerijeCheckBox.Checked, brzinaPostojanostChart);
        }

        private void prikazMaxSerija()
        {
            int indexMaxSerija = 0;
            prikazSerija(indexMaxSerija, maxSerijeCheckBox.Checked, brzinaProizvodnostChart);
            prikazSerija(indexMaxSerija, maxSerijeCheckBox.Checked, postojanostProizvodnostChart);
            prikazSerija(indexMaxSerija, maxSerijeCheckBox.Checked, brzinaPostojanostChart);
        }

        private void prikazMinSerija()
        {
            int indexMinSerija = 2;
            prikazSerija(indexMinSerija, minSerijeCheckBox.Checked, brzinaProizvodnostChart);
            prikazSerija(indexMinSerija, minSerijeCheckBox.Checked, postojanostProizvodnostChart);
            prikazSerija(indexMinSerija, minSerijeCheckBox.Checked, brzinaPostojanostChart);
        }

        private void prikazSerija(int indexSerijeZaSakrivanje, bool checkBoxIsChecked, Chart chart)
        {
            int ukupanBrojSerija = chart.Series.Count;

            for (int indexSerije = 0; indexSerije < ukupanBrojSerija; indexSerije++)
            {
                if (indexSerije == indexSerijeZaSakrivanje)
                {
                    chart.Series[indexSerije].Enabled = checkBoxIsChecked;
                    indexSerijeZaSakrivanje += 3;
                }
            }
        }

        private void izborGrafika()
        {
            int tabIndex = graficiTabControl.SelectedIndex;
            switch (tabIndex)
            {
                case 0:
                    postavljanjeIzabranogGrafika(brzinaProizvodnostChart);
                    break;
                case 1:
                    postavljanjeIzabranogGrafika(postojanostProizvodnostChart);
                    break;
                case 2:
                    postavljanjeIzabranogGrafika(brzinaPostojanostChart);
                    break;
                default:
                    postavljanjeIzabranogGrafika(brzinaProizvodnostChart);
                    break;
            }
        }

        private void racunanjeTacakaZaDijagramPostojanostProizvodnost()
        {
            double brzina = 0;
            double pomak = 0;
            double dubina = 0;
            double postojanostAlata = 0;
            double korekcioniFaktor = 0;
            double proizvodnost = 0;
            string oznakaSerije = "";
            for (int listBrzineIndex = 0; listBrzineIndex < izracunatiRezimi.Brzine.Count; listBrzineIndex++)
            {
                brzina = izracunatiRezimi.Brzine[listBrzineIndex];
                pomak = izracunatiRezimi.Pomaci[listBrzineIndex];
                dubina = izracunatiRezimi.Dubine[listBrzineIndex];
                int numOfListElements = preporukeZaPostojanost.ListaPreporukaZaPostojanost.Count;
                for (int listIndex = 0; listIndex < numOfListElements; listIndex++)
                {
                    PointCoordinates point = new PointCoordinates();
                    postojanostAlata = preporukeZaPostojanost.ListaPreporukaZaPostojanost[listIndex].PostojanostAlata;
                    korekcioniFaktor = preporukeZaPostojanost.ListaPreporukaZaPostojanost[listIndex].KorekcioniFaktor;
                    proizvodnost = brzina * pomak * dubina;
                    point.XCoordinate = postojanostAlata;
                    point.YCoordinate = Math.Round(korekcioniFaktor * proizvodnost, 1, MidpointRounding.AwayFromZero);
                    listaTacaka.Add(point);
                }
                oznakaSerije = plocica.OznakaPlocice + "\n" + "a = " + dubina + "   s = " + pomak;
                graphDrawPostojanostProizvodnost.DrawGraph(listaTacaka, oznakaSerije, plocica.OznakaPlocice, 0);
                listaTacaka.Clear();
                prikazSerija();
            }
        }

        private void racunanjeTacakaZaDijagramBrzinaPostojanost()
        {
            double brzina = 0;
            double pomak = 0;
            double dubina = 0;
            double postojanostAlata = 0;
            double korekcioniFaktor = 0;
            string oznakaSerije = "";
            for (int listBrzineIndex = 0; listBrzineIndex < izracunatiRezimi.Brzine.Count; listBrzineIndex++)
            {
                brzina = izracunatiRezimi.Brzine[listBrzineIndex];
                pomak = izracunatiRezimi.Pomaci[listBrzineIndex];
                dubina = izracunatiRezimi.Dubine[listBrzineIndex];
                int numOfListElements = preporukeZaPostojanost.ListaPreporukaZaPostojanost.Count;
                for (int listIndex = 0; listIndex < numOfListElements; listIndex++)
                {
                    PointCoordinates point = new PointCoordinates();
                    postojanostAlata = preporukeZaPostojanost.ListaPreporukaZaPostojanost[listIndex].PostojanostAlata;
                    korekcioniFaktor = preporukeZaPostojanost.ListaPreporukaZaPostojanost[listIndex].KorekcioniFaktor;
                    point.XCoordinate = Math.Round(korekcioniFaktor * brzina, 1, MidpointRounding.AwayFromZero);
                    point.YCoordinate = postojanostAlata;
                    listaTacaka.Add(point);
                }
                oznakaSerije = plocica.OznakaPlocice + "\n" + "a = " + dubina + "   s = " + pomak;
                graphDrawBrzinaPostojanost.DrawGraph(listaTacaka, oznakaSerije, plocica.OznakaPlocice, 3);
                listaTacaka.Clear();
                prikazSerija();
            }
        }

        private double racunanjeGranicneVrednostiPomaka(double radijus, double kvalitetObradjenePovrsine)
        {
            return Math.Round(Math.Sqrt(kvalitetObradjenePovrsine * (18 * Math.Sqrt(3 * radijus / 10.0)) / 1000), 3, MidpointRounding.AwayFromZero);
        }

        private void prikazLabela(Chart chart, bool checkBoxIsChecked)
        {
            foreach (Series series in chart.Series)
            {
                foreach (DataPoint point in series.Points)
                {
                    point.IsValueShownAsLabel = checkBoxIsChecked;
                }
            }
        }

        private void prikazLabelaZaSveGrafike()
        {
            bool checkedState = ukljanjanjeLabelaCheckBox.Checked;
            prikazLabela(brzinaPostojanostChart, checkedState);
            prikazLabela(postojanostProizvodnostChart, checkedState);
            prikazLabela(brzinaProizvodnostChart, checkedState);
        }

        private void popunjavanjePostojanostiAlata()
        {
            foreach (PreporukeZaPostojanostAlata postojanost in preporukeZaPostojanost.ListaPreporukaZaPostojanost)
            {
                postojanostCBox.Items.Add(postojanost.PostojanostAlata.ToString());
            }
        }

        private void postojanostCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexPostojanosti = postojanostCBox.SelectedIndex;
            double korekcioniFaktor = preporukeZaPostojanost.ListaPreporukaZaPostojanost[indexPostojanosti].KorekcioniFaktor;
            double novaVrednostBrzine = Math.Round(granicnaVrednostBrzine * korekcioniFaktor, 0, MidpointRounding.AwayFromZero);
            brzinaUZavisnostiOdPostojanostiTextBox.Text = novaVrednostBrzine.ToString();
        }

        private void postaviSlikuDrzacaAlata()
        {
            drzacAlataPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            drzacAlataPictureBox.BackColor = Color.White;
            drzacAlataPictureBox.Image = slikeZaFormuSt.ListaSlikaZaFormu[1];
        }

        #endregion

        public Form1()
        {
            InitializeComponent();

            if (System.IO.File.Exists("config.txt"))
            {
                string[] lines = System.IO.File.ReadAllLines("config.txt");
                if (lines.Length == 0)
                {
                    MessageBox.Show("Configuration file config.txt is empty");
                    this.Enabled = false;
                }
                else
                {
                    connString = lines[0];
                }
            }
            else
            {
                MessageBox.Show("Configuration file doesn't exist");
                this.Enabled = false;
            }

            ConnToDB.ConnectionString = connString;
            materijaliSt = new MaterijaliStorage();
            vrstaObradeSt = new VrstaObradeStorage();
            tipObradeSt = new TipObradeStorage();
            oblikPlociceSt = new ObliciPlocicaStorage();
            kvalitetObradeSt = new KvalitetObradeStorage();
            dodaciSt = new DodaciStorage();
            pripremaciSt = new PripremciStorage();
            kvalitetiRPlSt = new KvalitetiReznihPlocicaStorage();
            prepRezimiSt = new PrepRezimiStorage();
            drzaciSt = new DrzaciAlataStorage();
            masineSt = new MasineDataStorage();
            preporukeISOPSt = new PreporukeISOPStorage();
            gradacijeObradeSt = new GradacijeObradaStorage();
            interpolacijaBrzina = new InterpolacijaBrzina();
            filterPlocica = new FilterReznihPlocica();
            listaPlocica = new List<ReznaPlocica>();
            graphDrawBrzinaProizvodnost = new GraphDrawing();
            graphDrawPostojanostProizvodnost = new GraphDrawing();
            graphDrawBrzinaPostojanost = new GraphDrawing();
            preporukeZaPostojanost = new PreporukeZaPostojanostAlataStorage();
            slikeZaFormuSt = new SlikeZaFormuStorage();
            listaTacaka = new List<PointCoordinates>();
            rezimi = new RezimiObrade();
            plocica = new ReznaPlocica();
            izracunatiRezimi = new IzracunatiRezimi();
            izracunatiPrepRezimi = new IzracunatiPreporuceniRezimi();
            preporuceniRezim = new PreporuceniRezim();
            granicnaVrednostDubine = new GranicnaVrednostDubneRezanja();
            racunanjeParametaraRezanja = new RacunanjeParametaraRezanja();

            graphDrawBrzinaProizvodnost.SetNamesOfAxisAndLabels("Brzina rezanja v (m/min)", "Proizvodnost Q (cm³/min)", "v", "Q", brzinaProizvodnostChart);
            graphDrawPostojanostProizvodnost.SetNamesOfAxisAndLabels("Postojanost alata T (min)", "Proizvodnost Q (cm³/min)", "T", "Q", postojanostProizvodnostChart);
            graphDrawBrzinaPostojanost.SetNamesOfAxisAndLabels("Brzina rezanja v (m/min)", "Postojanost alata T (min)", "v", "T", brzinaPostojanostChart);

            maxSerijeCheckBox.Checked = true;
            preporuceneSerijeCheckBox.Checked = true;
            minSerijeCheckBox.Checked = true;
            ukljanjanjeLabelaCheckBox.Checked = true;

            fillComboBoxAndSetToDropDownList(materialComBox, materijaliSt.ListOfDictElements);
            fillComboBoxAndSetToDropDownList(vrstaObradeComboBox, vrstaObradeSt.ListOfDictElements);
            fillComboBoxAndSetToDropDownList(klasaKvalitetaComboBox, kvalitetObradeSt.ListOfDictElements);



            granicnaVrednostDubine.ListaGradacijaObrade = gradacijeObradeSt.ListaGradacija;
        }

        #region Events

        private void materialComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            materijaliSt.ChooseElement(materialComBox.Text);
            MaterijalObratka materijal = MaterijaliStorage.IzabraniMaterijal;
            hardnessTextBox.Text = materijal.HB.ToString(); ;
            cForceTextBox.Text = materijal.SpecificnaSilaR.ToString();
            racunanjeParametaraRezanja.SpecificnaSilaRezanja = materijal.SpecificnaSilaR;
        }

        private void vrstaObradeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            vrstaObradeSt.ChooseElement(vrstaObradeComboBox.Text);
            fillComboBoxAndSetToDropDownList(tipObradeComboBox, tipObradeSt.ListOfDictElements);
        }

        private void tipObradeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipObradeSt.ChooseElement(tipObradeComboBox.Text);
            izborObradePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            izborObradePictureBox.BackColor = Color.White;
            izborObradePictureBox.Image = TipObradeStorage.IzabraniTipObrade.SlikaObr;

            izborObratkaPictureBox.Image = slikeZaFormuSt.ListaSlikaZaFormu[0];
        }

        private void klasaKvalitetaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            kvalitetObradeSt.ChooseElement(klasaKvalitetaComboBox.Text);
            raMaxTextBox.Text = KvalitetObradeStorage.IzabraniKvalitet.RaMax.ToString();
            granicnaVrednostDubine.GradacijaObrade = KvalitetObradeStorage.IzabraniKvalitet.GradacijaObrade;
        }

        private void precnikObratkaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumberInputControl.numberInputControl(sender, e);
        }

        private void precnikObratkaTextBox_TextChanged(object sender, EventArgs e)
        {
            IzaberiDodatke();
        }

        private void duzinaObradeTextBox_TextChanged(object sender, EventArgs e)
        {
            IzaberiDodatke();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fillComboBoxAndSetToDropDownList(kvalitetiCBox, preporukeISOPSt.ListOfDictElements);

        }

        private void kvalitetiCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            reznePlociceCheckedListBox.Items.Clear();
            brisanjeSerijaGrafika();
            prepRezimiSt.OdabirPlocice(kvalitetiCBox.Text);
            PreporukeZaMaterijal preporuke = PreporukeISOPStorage.IzabranePreporuke;
            interpolacijaBrzina.Preporuke = preporuke;
            fillCheckedListBox(reznePlociceCheckedListBox, prepRezimiSt.ListOfDictElements);
            napuniListuPlocica();
            filterPlocica.ListaPlocica = listaPlocica;
            popunjavanjeCheckedListBoxova();
            graficiTabControl.SelectedIndex = 0;
            popunjavanjePostojanostiAlata();
            postojanostCBox.SelectedIndex = 1;
        }

        private void brisanjeSerijaGrafika()
        {
            brzinaProizvodnostChart.Series.Clear();
            postojanostProizvodnostChart.Series.Clear();
            brzinaPostojanostChart.Series.Clear();
        }

        private void oblikPlociceCLBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            brisanjeSerijaGrafika();
            popunjavanjeListeDel = popuniListuOblikaPlocica;
            odlaganjeItemCheckEventa(sender, e);
        }

        private void ledjniUgaoCLBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            brisanjeSerijaGrafika();
            popunjavanjeListeDel = popuniListuLedjnihUglova;
            odlaganjeItemCheckEventa(sender, e);
        }

        private void tolerancijaCLBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            brisanjeSerijaGrafika();
            popunjavanjeListeDel = popuniListuTolerancija;
            odlaganjeItemCheckEventa(sender, e);
        }

        private void tipPlociceCLBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            brisanjeSerijaGrafika();
            popunjavanjeListeDel = popuniListuTipovaPlocica;
            odlaganjeItemCheckEventa(sender, e);
        }

        private void duzinaRezneIviceCLBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            brisanjeSerijaGrafika();
            popunjavanjeListeDel = popuniListuDuzina;
            odlaganjeItemCheckEventa(sender, e);
        }

        private void debljinaCLBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            brisanjeSerijaGrafika();
            popunjavanjeListeDel = popuniListuDebljina;
            odlaganjeItemCheckEventa(sender, e);
        }

        private void radijusCLBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            brisanjeSerijaGrafika();
            popunjavanjeListeDel = popuniListuRadijusa;
            odlaganjeItemCheckEventa(sender, e);
        }

        private void kvalitetiCLBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            brisanjeSerijaGrafika();
            popunjavanjeListeDel = popuniListuKvaliteta;
            odlaganjeItemCheckEventa(sender, e);
        }

        private void reznePlociceCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                crtanjeGrafikaIPopunjavanje();
            }
            else
            {
                uklanjanjeSerijaSaGrafika(reznePlociceCheckedListBox.Items[e.Index].ToString(), brzinaProizvodnostChart);
                uklanjanjeSerijaSaGrafika(reznePlociceCheckedListBox.Items[e.Index].ToString(), postojanostProizvodnostChart);
                uklanjanjeSerijaSaGrafika(reznePlociceCheckedListBox.Items[e.Index].ToString(), brzinaPostojanostChart);
            }
        }

        private void maxSerijeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            prikazMaxSerija();
        }

        private void preporuceneSerijeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            prikazPrepurucenihSerija();
        }

        private void minSerijeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            prikazMinSerija();
        }

        #region Zoom
        //protected override void OnMouseWheel(MouseEventArgs e)
        //{
        //    izborGrafika();
        //    try
        //    {
        //        if (e.Delta < 0)
        //        {
        //            izabraniChart.ChartAreas[0].AxisX.ScaleView.ZoomReset();
        //            izabraniChart.ChartAreas[0].AxisY.ScaleView.ZoomReset();
        //        }

        //        if (e.Delta > 0)
        //        {
        //            #region stari kod
        //            //double xMin = izabraniChart.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
        //            //double xMax = izabraniChart.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
        //            //double yMin = izabraniChart.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
        //            //double yMax = izabraniChart.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

        //            //Point p1 = System.Windows.Forms.Cursor.Position;
        //            //Point po = izabraniChart.PointToClient(p1);
        //            //double xCoord = izabraniChart.ChartAreas[0].AxisX.PixelPositionToValue(po.X);
        //            //double yCoord = izabraniChart.ChartAreas[0].AxisY.PixelPositionToValue(po.Y);

        //            //double posXStart = Math.Round(xCoord - (xMax - xMin) / 2, 0, MidpointRounding.AwayFromZero);
        //            //double posXFinish = Math.Round(xCoord + (xMax - xMin) / 2, 0, MidpointRounding.AwayFromZero);
        //            //double posYStart = Math.Round(yCoord - (yMax - yMin) / 2, 0, MidpointRounding.AwayFromZero);
        //            //double posYFinish = Math.Round(yCoord + (yMax - yMin) / 2, 0, MidpointRounding.AwayFromZero);

        //            //if (posXStart < 0)
        //            //{
        //            //    posXStart = 0;
        //            //}
        //            //if (posYStart < 0)
        //            //{
        //            //    posYStart = 0;
        //            //}

        //            //izabraniChart.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
        //            //izabraniChart.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);
        //            #endregion
        //            double xMin = izabraniChart.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
        //            xMinTb.Text = xMin.ToString();
        //            double xMax = izabraniChart.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
        //            xMaxTb.Text = xMax.ToString();
        //            double yMin = izabraniChart.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
        //            yMinTb.Text = yMin.ToString();
        //            double yMax = izabraniChart.ChartAreas[0].AxisY.ScaleView.ViewMaximum;
        //            yMaxTb.Text = yMax.ToString();

        //            Point p1 = System.Windows.Forms.Cursor.Position;
        //            Point po = izabraniChart.PointToClient(p1);
        //            double xCoord = izabraniChart.ChartAreas[0].AxisX.PixelPositionToValue(po.X);
        //            double yCoord = izabraniChart.ChartAreas[0].AxisY.PixelPositionToValue(po.Y);
        //            pxTB.Text = xCoord.ToString();
        //            pyTB.Text = yCoord.ToString();
        //            double a = 0.25;
        //            //double zoomX = (xMax / xMin) / a;
        //            //double zoomY = (yMax / yMin) / a;
        //            double xLenght = (xMax - xMin)*a;
        //            double yLenght = (yMax - yMin)*a;
        //            double newPosXMin = Math.Round(xCoord - xLenght, 0, MidpointRounding.AwayFromZero);
        //            xMinSTb.Text = newPosXMin.ToString();
        //            double newPosXMax = Math.Round(xCoord + xLenght, 0, MidpointRounding.AwayFromZero);
        //            xMaxSTb.Text = newPosXMax.ToString();
        //            double newPosYMin = Math.Round(yCoord - yLenght, 0, MidpointRounding.AwayFromZero);
        //            yMinSTb.Text = newPosYMin.ToString();
        //            double newPosYMax = Math.Round(yCoord + yLenght, 0, MidpointRounding.AwayFromZero);
        //            yMaxSTb.Text = newPosYMax.ToString();

        //            //izabraniChart.ChartAreas[0].AxisX.ScaleView.Zoom(newPosXMin, newPosXMax);
        //            //izabraniChart.ChartAreas[0].AxisY.ScaleView.Zoom(newPosYMin, newPosYMax);
        //            izabraniChart.ChartAreas[0].AxisX.ScaleView.Zoom(xCoord * 0.80, xCoord * 1.2);
        //            izabraniChart.ChartAreas[0].AxisY.ScaleView.Zoom(yCoord *0.80 , yCoord * 1.2);

        //        }
        //    }
        //    catch { }
        //    base.OnMouseWheel(e);
        //}
        #endregion
        private void ukljanjanjeLabelaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            prikazLabelaZaSveGrafike();
        }

        private void graficiTabControl_Selected(object sender, TabControlEventArgs e)
        {
            izborGrafika();
        }

        private void izaberiReznuPlocicuButton_Click(object sender, EventArgs e)
        {
            drzaciAlataListBox.Items.Clear();
            foreach (string nazivDrzaca in drzaciSt.ListOfDictElements)
            {
                drzaciAlataListBox.Items.Add(nazivDrzaca);
            }


            ipDuzinaTextBox.Text = duzinaObradeNumericUpDown.Value.ToString();
            ipKvalitetTextBox.Text = klasaKvalitetaComboBox.Text;
            ipMaterijalTextBox.Text = materialComBox.Text;
            ipPrecnikOTextBox.Text = precnikObratkaNumericUpDown.Value.ToString();
            ipPrecnikPTextBox.Text = precnikPripremkaTextBox.Text;
            ipTipTextBox.Text = tipObradeComboBox.Text;
            ipVrstaTextBox.Text = vrstaObradeComboBox.Text;
            ipPlocicaTextBox.Text = oznakaRPlTBox.Text;
            ipMasinaTextBox.Text = nazivMasineTextBox.Text;
            ipPomakTextBox.Text = izabraniPomakNumericUpDown.Value.ToString();
            ipBrzinaTextBox.Text = izabranaBrzinaNumericUpDown.Value.ToString();
            ipDubinaTextBox.Text = izabranaDubinaNumericUpDown.Value.ToString();

        }
        #endregion

        private void drzaciAlataListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string oznakaDrzaca = drzaciAlataListBox.GetItemText(drzaciAlataListBox.SelectedItem);
            drzaciSt.ChooseElement(oznakaDrzaca);
            DrzacAlata drzac = DrzaciAlataStorage.izabraniDrzacAlata;
            oznakaDrzacaTextBox.Text = drzac.OznakaDrzaca;
            nacinPricvrscivanjaTextBox.Text = drzac.PricvrscivanjePlocice.ToString();
            oblikPlociceDrzacTextBox.Text = drzac.Oblik.OblikPloc.ToString();
            napadniUgaoDrzacTextBox.Text = drzac.NapadniUgao.ToString();
            ledjniUgaoDrzacTextBox.Text = drzac.LedjniUgao.ToString();
            smerRezanjaTextBox.Text = drzac.Smer.ToString();
            visinaDrzacaTextBox.Text = drzac.VisinaDrzaca.ToString();
            sirinaDrzacaTextBox.Text = drzac.SirinaDrzaca.ToString();
            duzinaAlataTextBox.Text = drzac.DuzinaAlata.ToString();
            duzinaRezneIviceDrzacTextBox.Text = drzac.IC.ToString();
            bTextBox.Text = drzac.SirinaDrzaca.ToString();
            f1TextBox.Text = drzac.F1.ToString();
            hTextBox.Text = drzac.VisinaDrzaca.ToString();
            h1TextBox.Text = drzac.H1.ToString();
            l1TextBox.Text = drzac.VrednostDuzineAlata.ToString();
            l3TextBox.Text = drzac.L3.ToString();
            gamaTextBox.Text = drzac.Gama.ToString();
            lambdaTextBox.Text = drzac.Lambda.ToString();
            krTextBox.Text = drzac.VrednostNapadnogUgla.ToString();
            postaviSlikuDrzacaAlata();
            popunjavanjeListeMasina();
            ipDrzacTextBox.Text = oznakaDrzacaTextBox.Text;
        }

        private void popunjavanjeListeMasina()
        {
            masineAlatkeListBox.Items.Clear();
            double pm = convertStringToDouble(potrebnaSnagaMotoraTextBox.Text);
            double precnik = convertStringToDouble(precnikPripremkaTextBox.Text);
            double duzina = (double)duzinaObradeNumericUpDown.Value;
            double n = convertStringToDouble(potrebanBrojObrtajaTextBox.Text);
            masineSt.PostavljanjeUslovaZaIzborMasine(pm, precnik, duzina, n);
            foreach (var item in masineSt.ListOfDictElements)
            {
                masineAlatkeListBox.Items.Add(item);
            }
        }

        private void masineAlatkeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string oznakaMasine = masineAlatkeListBox.GetItemText(masineAlatkeListBox.SelectedItem);
            masineSt.ChooseElement(oznakaMasine);
            MasinaA masina = MasineDataStorage.IzabranaMasina;
            nazivMasineTextBox.Text = masina.NazivMasine;
            pmMaxTextBox.Text = masina.Pm.ToString();
            nMaxTextBox.Text = masina.NMax.ToString();
            dOptTextBox.Text = masina.DOpt.ToString();
            dMaxTextBox.Text = masina.DMax.ToString();
            lMaxTextBox.Text = masina.LMax.ToString();
            stepenITextBox.Text = masina.StepenIskoristenja.ToString();
            ipMasinaTextBox.Text = nazivMasineTextBox.Text;
        }

        private void label124_Click(object sender, EventArgs e)
        {

        }

        private void label126_Click(object sender, EventArgs e)
        {

        }

        private void label128_Click(object sender, EventArgs e)
        {

        }

        private void label127_Click(object sender, EventArgs e)
        {

        }

        private void label129_Click(object sender, EventArgs e)
        {

        }

        private void groupBox16_Enter(object sender, EventArgs e)
        {

        }

        private void nazivMasineTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void precnikObratkaNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            IzaberiDodatke();
        }

        private void duzinaObradeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            IzaberiDodatke();
        }

        private void izabranaBrzinaNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            double vrednost = (double)izabranaBrzinaNumericUpDown.Value;
            if (vrednost > 1)
            {
                racunanjeParametaraRezanja.Brzina = vrednost;
                popunjavanjeParametaraRezanjaIPotrebnihUslova();
            }
        }

        private void izabraniPomakNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            double vrednost = (double)izabraniPomakNumericUpDown.Value;
            if (vrednost > 0)
            {
                racunanjeParametaraRezanja.Pomak = vrednost;
                popunjavanjeParametaraRezanjaIPotrebnihUslova();
            }
        }

        private void izabranaDubinaNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            double vrednost = (double)izabranaDubinaNumericUpDown.Value;
            if (vrednost > 0)
            {
                racunanjeParametaraRezanja.Dubina = vrednost;
                popunjavanjeParametaraRezanjaIPotrebnihUslova();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }


    }
}
