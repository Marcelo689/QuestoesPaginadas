class SelectPagina {

    public static um = 0;
    public static dois = 1;
    public static quatro = 2;
    public static seis = 3;
    public static oito = 4;
    public static dez = 5;
    public static opcoes: SelectPagina = new SelectPagina();
}

class Instancia {
    static listaPaginada() {
        this.paginar();
    }

    static elementoNumeroPaginaAtual: JQuery;
    static elementoQuestoes: JQuery;
    static btnPrevElement: JQuery<HTMLElement>;
    static btnNextElement: JQuery<HTMLElement>;
    static elementoSelectNumeroPaginaAtual: JQuery<HTMLElement>;
    static listaPaginadaElement: any;
    static paginaExibidaElemento: any;

    public static selectNumeroPaginaElemento() {
        this.elementoSelectNumeroPaginaAtual = $("#numeroQuestaoPorPagina");
    }
    static getSelectNumeroPaginaElemento() {
        return this.elementoSelectNumeroPaginaAtual;
    }

    public  static questoesElemento() {
        this.elementoQuestoes = $(".container-questao");
    }
    static getQuestoesElemento() {
        return this.elementoQuestoes;
    }

    public static BtnNext() {
        this.btnNextElement = $("#btn-next");
    }
    static getBtnNext() {
        return this.btnNextElement;
    }

    public static BtnPrev() {
        this.btnPrevElement = $("#btn-prev");
    }
    static getBtnPrev() {
        return this.btnPrevElement;
    }

    public static numeroPaginaElemento() {
        this.elementoNumeroPaginaAtual = $("#pagina-atual");
    }
    static getNumeroPaginaElemento() {
        return this.elementoNumeroPaginaAtual;
    }


    static atualizarPaginaExibida() {
        this.ocultarTodasQuestoes();
        this.mostrarPaginaExibida();
    }

    static mostrarPaginaExibida() {
        Instancia.paginaExibidaElemento.map(e => $(e).hide());
    }

    static ocultarTodasQuestoes() {
        Instancia.listaPaginadaElement.map(e => $(e).hide());
    }

    static paginar() {
        var elementoNumeroQuestoesPorPagina = Instancia.getNumeroPaginaElemento();
        var elementoQuestoes = Instancia.getQuestoesElemento();
        var numeroPorPagina: number = Number(elementoNumeroQuestoesPorPagina.val());

        Instancia.listaPaginadaElement = Instancia.fatiarListaEmPedacos(elementoQuestoes, numeroPorPagina);
    }

    static fatiarListaEmPedacos(lista: any, tamanhoPorPedaco: number) {
        var listaContemElementos = lista.length != 0;

        var listaFatiada = [];
        while (listaContemElementos) {
            var fatia = lista.splice(0, tamanhoPorPedaco);
            listaFatiada.push(fatia);
            listaContemElementos = lista.length != 0;
        }

        return listaFatiada;
    }

    public static instanciaPaginaExibida() {
        var numeroPaginaAtual = Number(Instancia.elementoNumeroPaginaAtual.val());
        var indiceNextPagina = numeroPaginaAtual - 1;
        Instancia.paginaExibidaElemento = Instancia.listaPaginadaElement[indiceNextPagina];
    }
}

class IndexController {

    public onLoad() {
        this.instanciarObjetosPagina();
        this.adicionarEventosObjetosPagina();
    }

    public adicionarEventosObjetosPagina() {
        var isso = this;

        Instancia.getBtnPrev().click(function () {
            Instancia.paginar();
            isso.prevPagina();
        });

        Instancia.getBtnNext().click(function () {
            Instancia.paginar();
            isso.nextPagina();
        });

        Instancia.getSelectNumeroPaginaElemento().change(function (e) {
            Instancia.instanciaPaginaExibida();
        });
    }


    public nextPagina() {
        var numeroPaginaAtual = Number(Instancia.elementoNumeroPaginaAtual.val());
        const possivelPaginar = numeroPaginaAtual < Instancia.listaPaginadaElement.length;

        if (possivelPaginar) {
            var indiceNextPagina = numeroPaginaAtual;
            Instancia.paginaExibidaElemento = Instancia.listaPaginadaElement[indiceNextPagina];
        }

        Instancia.atualizarPaginaExibida();
    }

    public prevPagina() {
        var numeroPaginaAtual = Number(Instancia.elementoNumeroPaginaAtual.val());
        const possivelPaginar = numeroPaginaAtual > 1;

        if (possivelPaginar) {
            var indicePagina = numeroPaginaAtual - 1;
            var indicePrevPagina = indicePagina - 1;
            Instancia.paginaExibidaElemento = Instancia.listaPaginadaElement[indicePrevPagina];
        }

        Instancia.atualizarPaginaExibida();
    }

    public instanciarObjetosPagina() {
        Instancia.questoesElemento();
        Instancia.selectNumeroPaginaElemento();
        Instancia.numeroPaginaElemento();
        Instancia.listaPaginadaElement();
        Instancia.atualizarPaginaExibida();
        Instancia.BtnPrev();
        Instancia.BtnNext();
    }

}

var controller = new IndexController();

document.addEventListener("DOMContentLoaded", function () {
    controller.onLoad();
});