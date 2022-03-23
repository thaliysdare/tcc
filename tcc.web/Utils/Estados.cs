using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tcc.web.Utils
{
    public class Estados
    {
        public static List<SelectListItem> ListaEstados = new List<SelectListItem>() {
            new SelectListItem() {Value =  "AC", Text = "ACRE"},
            new SelectListItem() {Value =  "AL", Text = "ALAGOAS"},
            new SelectListItem() {Value =  "AP", Text = "AMAPÁ"},
            new SelectListItem() {Value =  "AM", Text = "AMAZONAS"},
            new SelectListItem() {Value =  "BA", Text = "BAHIA"},
            new SelectListItem() {Value =  "CE", Text = "CEARÁ"},
            new SelectListItem() {Value =  "DF", Text = "DISTRITO FEDERAL"},
            new SelectListItem() {Value =  "ES", Text = "ESPIRITO SANTO"},
            new SelectListItem() {Value =  "GO", Text = "GOIÁS"},
            new SelectListItem() {Value =  "MA", Text = "MARANHÃO"},
            new SelectListItem() {Value =  "MT", Text = "MATO GROSSO"},
            new SelectListItem() {Value =  "MS", Text = "MATO GROSSO DO SUL"},
            new SelectListItem() {Value =  "MG", Text = "MINAS GERAIS"},
            new SelectListItem() {Value =  "PA", Text = "PARÁ"},
            new SelectListItem() {Value =  "PB", Text = "PARAÍBA"},
            new SelectListItem() {Value =  "PR", Text = "PARANÁ"},
            new SelectListItem() {Value =  "PE", Text = "PERNAMBUCO"},
            new SelectListItem() {Value =  "PI", Text = "PIAUÍ"},
            new SelectListItem() {Value =  "RJ", Text = "RIO DE JANEIRO"},
            new SelectListItem() {Value =  "RN", Text = "RIO GRANDE DO NORTE"},
            new SelectListItem() {Value =  "RS", Text = "RIO GRANDE DO SUL"},
            new SelectListItem() {Value =  "RO", Text = "RONDÔNIA"},
            new SelectListItem() {Value =  "RR", Text = "RORAIMA"},
            new SelectListItem() {Value =  "SC", Text = "SANTA CATARINA"},
            new SelectListItem() {Value =  "SP", Text = "SÃO PAULO"},
            new SelectListItem() {Value =  "SE", Text = "SERGIPE"},
            new SelectListItem() {Value =  "TO", Text = "TOCANTIS"}
        };
    }
}
