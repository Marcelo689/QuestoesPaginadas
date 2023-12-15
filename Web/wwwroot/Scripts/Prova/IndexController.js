var SelectPagina = /** @class */ (function () {
    function SelectPagina() {
    }
    SelectPagina.um = 0;
    SelectPagina.dois = 1;
    SelectPagina.quatro = 2;
    SelectPagina.seis = 3;
    SelectPagina.oito = 4;
    SelectPagina.dez = 5;
    SelectPagina.opcoes = new SelectPagina();
    return SelectPagina;
}());
var Instancia = /** @class */ (function () {
    function Instancia() {
    }
    Instancia.listaPaginada = function () {
        this.paginar();
    };
    Instancia.selectNumeroPaginaElemento = function () {
        this.elementoSelectNumeroPaginaAtual = $("#numeroQuestaoPorPagina");
    };
    Instancia.getSelectNumeroPaginaElemento = function () {
        return this.elementoSelectNumeroPaginaAtual;
    };
    Instancia.questoesElemento = function () {
        this.elementoQuestoes = $(".container-questao");
    };
    Instancia.getQuestoesElemento = function () {
        return this.elementoQuestoes;
    };
    Instancia.BtnNext = function () {
        this.btnNextElement = $("#btn-next");
    };
    Instancia.getBtnNext = function () {
        return this.btnNextElement;
    };
    Instancia.BtnPrev = function () {
        this.btnPrevElement = $("#btn-prev");
    };
    Instancia.getBtnPrev = function () {
        return this.btnPrevElement;
    };
    Instancia.numeroPaginaElemento = function () {
        this.elementoNumeroPaginaAtual = $("#pagina-atual");
    };
    Instancia.getNumeroPaginaElemento = function () {
        return this.elementoNumeroPaginaAtual;
    };
    Instancia.atualizarPaginaExibida = function () {
        this.ocultarTodasQuestoes();
        this.mostrarPaginaExibida();
    };
    Instancia.mostrarPaginaExibida = function () {
        Instancia.paginaExibidaElemento.map(function (e) { return $(e).hide(); });
    };
    Instancia.ocultarTodasQuestoes = function () {
        Instancia.listaPaginadaElement.map(function (e) { return $(e).hide(); });
    };
    Instancia.paginar = function () {
        var elementoNumeroQuestoesPorPagina = Instancia.getNumeroPaginaElemento();
        var elementoQuestoes = Instancia.getQuestoesElemento();
        var numeroPorPagina = Number(elementoNumeroQuestoesPorPagina.val());
        Instancia.listaPaginadaElement = Instancia.fatiarListaEmPedacos(elementoQuestoes, numeroPorPagina);
    };
    Instancia.fatiarListaEmPedacos = function (lista, tamanhoPorPedaco) {
        var listaContemElementos = lista.length != 0;
        var listaFatiada = [];
        while (listaContemElementos) {
            var fatia = lista.splice(0, tamanhoPorPedaco);
            listaFatiada.push(fatia);
            listaContemElementos = lista.length != 0;
        }
        return listaFatiada;
    };
    Instancia.instanciaPaginaExibida = function () {
        var numeroPaginaAtual = Number(Instancia.elementoNumeroPaginaAtual.val());
        var indiceNextPagina = numeroPaginaAtual - 1;
        Instancia.paginaExibidaElemento = Instancia.listaPaginadaElement[indiceNextPagina];
    };
    return Instancia;
}());
var IndexController = /** @class */ (function () {
    function IndexController() {
    }
    IndexController.prototype.onLoad = function () {
        this.instanciarObjetosPagina();
        this.adicionarEventosObjetosPagina();
    };
    IndexController.prototype.adicionarEventosObjetosPagina = function () {
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
    };
    IndexController.prototype.nextPagina = function () {
        var numeroPaginaAtual = Number(Instancia.elementoNumeroPaginaAtual.val());
        var possivelPaginar = numeroPaginaAtual < Instancia.listaPaginadaElement.length;
        if (possivelPaginar) {
            var indiceNextPagina = numeroPaginaAtual;
            Instancia.paginaExibidaElemento = Instancia.listaPaginadaElement[indiceNextPagina];
        }
        Instancia.atualizarPaginaExibida();
    };
    IndexController.prototype.prevPagina = function () {
        var numeroPaginaAtual = Number(Instancia.elementoNumeroPaginaAtual.val());
        var possivelPaginar = numeroPaginaAtual > 1;
        if (possivelPaginar) {
            var indicePagina = numeroPaginaAtual - 1;
            var indicePrevPagina = indicePagina - 1;
            Instancia.paginaExibidaElemento = Instancia.listaPaginadaElement[indicePrevPagina];
        }
        Instancia.atualizarPaginaExibida();
    };
    IndexController.prototype.instanciarObjetosPagina = function () {
        Instancia.questoesElemento();
        Instancia.selectNumeroPaginaElemento();
        Instancia.numeroPaginaElemento();
        Instancia.listaPaginadaElement();
        Instancia.atualizarPaginaExibida();
        Instancia.BtnPrev();
        Instancia.BtnNext();
    };
    return IndexController;
}());
var controller = new IndexController();
document.addEventListener("DOMContentLoaded", function () {
    controller.onLoad();
});
//# sourceMappingURL=IndexController.js.map