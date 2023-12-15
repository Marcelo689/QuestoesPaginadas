var SelectPagina = /** @class */ (function () {
    function SelectPagina() {
    }
    SelectPagina.opcoes = [
        1, 2, 4, 6, 8, 10
    ];
    return SelectPagina;
}());
var Instancia = /** @class */ (function () {
    function Instancia() {
    }
    Instancia.listaPaginadaElemento = function () {
        this.preeencherListaPaginada();
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
        Instancia.paginaExibidaElemento.map(function (e) { return $(e).show(); });
    };
    Instancia.ocultarTodasQuestoes = function () {
        Instancia.listaPaginadaElement.map(function (e) { return $(e).hide(); });
    };
    Instancia.preeencherListaPaginada = function () {
        var elementoNumeroQuestoesPorPagina = Instancia.getSelectNumeroPaginaElemento();
        var elementoQuestoes = Instancia.getQuestoesElemento();
        var indiceSelectNumeroPaginaValor = Number(elementoNumeroQuestoesPorPagina.val());
        var numeroPorPagina = SelectPagina.opcoes[indiceSelectNumeroPaginaValor];
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
        Instancia.questoesElemento();
        return listaFatiada;
    };
    Instancia.paginaExibida = function () {
        var numeroPaginaAtual = Number(Instancia.elementoNumeroPaginaAtual.val());
        var indiceNextPagina = numeroPaginaAtual - 1;
        Instancia.paginaExibidaElemento = Instancia.listaPaginadaElement[indiceNextPagina];
        Instancia.atualizarPaginaExibida();
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
            isso.prevPagina();
            Instancia.preeencherListaPaginada();
            Instancia.paginaExibida();
        });
        Instancia.getBtnNext().click(function () {
            isso.nextPagina();
            Instancia.preeencherListaPaginada();
            Instancia.paginaExibida();
        });
        Instancia.getSelectNumeroPaginaElemento().change(function (e) {
            Instancia.preeencherListaPaginada();
            Instancia.paginaExibida();
        });
    };
    IndexController.prototype.nextPagina = function () {
        var numeroPaginaAtual = Number(Instancia.elementoNumeroPaginaAtual.val());
        var possivelPaginar = numeroPaginaAtual < Instancia.listaPaginadaElement.length;
        if (possivelPaginar) {
            var proximaPagina = numeroPaginaAtual + 1;
            Instancia.elementoNumeroPaginaAtual.val(proximaPagina);
        }
        Instancia.atualizarPaginaExibida();
    };
    IndexController.prototype.prevPagina = function () {
        var numeroPaginaAtual = Number(Instancia.elementoNumeroPaginaAtual.val());
        var possivelPaginar = numeroPaginaAtual > 1;
        if (possivelPaginar) {
            var prevPagina = numeroPaginaAtual - 1;
            Instancia.elementoNumeroPaginaAtual.val(prevPagina);
        }
        Instancia.atualizarPaginaExibida();
    };
    IndexController.prototype.instanciarObjetosPagina = function () {
        Instancia.questoesElemento();
        Instancia.selectNumeroPaginaElemento();
        Instancia.numeroPaginaElemento();
        Instancia.listaPaginadaElemento();
        Instancia.paginaExibida();
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