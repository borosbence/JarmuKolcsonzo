using JarmuKolcsonzo.Models;
using JarmuKolcsonzo.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarmuKolcsonzo.ViewInterfaces
{
    public interface IRendelesView
    {
        rendelesVM rendelesVM { get; set; }
        string errorUgyfelNev { set; }
        string errorJarmuRendszam { set; }
        decimal rendelesAr { set; }
        string ugyfelTelefonszam { set; }
        string ugyfelEmail { set; }
        string jarmuTipus { set; }
        int jarmuFerohely { set; }
        decimal ugyfelPont { get; set; }
        int jarmuDij { get; set; }
        string[] ugyfelList { get; set; }
        string[] jarmuList { get; set; }
        bool PontokFelhasznalva { get; }
    }
}
