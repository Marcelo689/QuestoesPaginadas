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
    Instancia.getPaginaExibidaElemento = function () {
        return this.paginaExibidaElemento;
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
        Instancia.preeencherListaPaginada();
        this.ocultarTodasQuestoes();
        this.mostrarPaginaExibida();
    };
    Instancia.mostrarPaginaExibida = function () {
        var paginaExibida = Instancia.getPaginaExibidaElemento();
        $(paginaExibida[0]).show();
    };
    Instancia.getNumeroPossivelPaginas = function () {
        var numeroTotalRegistros = this.elementoQuestoes.length;
        var indiceSelectNumeroPaginaAtual = Number(Instancia.getSelectNumeroPaginaElemento().val());
        var numeroRegistroPorPagina = SelectPagina.opcoes[indiceSelectNumeroPaginaAtual];
        var numeroTotalDePaginas = numeroTotalRegistros / numeroRegistroPorPagina;
        return numeroTotalDePaginas;
    };
    Instancia.ocultarTodasQuestoes = function () {
        var todasQuestoes = Instancia.getQuestoesElemento();
        todasQuestoes.hide();
    };
    Instancia.preeencherListaPaginada = function () {
        var elementoNumeroQuestoesPorPagina = Instancia.getSelectNumeroPaginaElemento();
        var elementoQuestoes = Instancia.getQuestoesElemento();
        var indiceSelectNumeroPaginaValor = Number(elementoNumeroQuestoesPorPagina.val());
        var numeroPorPagina = SelectPagina.opcoes[indiceSelectNumeroPaginaValor];
        Instancia.paginaExibidaElemento = Instancia.fatiarListaEmPedacos(elementoQuestoes, numeroPorPagina);
    };
    Instancia.getIndiceFatiaFromPaginaAtual = function (numeroPaginaAtual) {
        var dados = numeroPaginaAtual - 1;
        var dadosZerado = dados == 0;
        if (dadosZerado) {
            dados = 1;
        }
        return dados;
    };
    Instancia.getFatia = function (lista, indiceFatiaInicio, numeroRegistroPorPagina) {
        var fatia;
        if (indiceFatiaInicio == numeroRegistroPorPagina) {
            fatia = lista.slice(indiceFatiaInicio, numeroRegistroPorPagina + 1);
        }
        else {
            fatia = lista.slice(indiceFatiaInicio, numeroRegistroPorPagina);
        }
        return fatia;
    };
    Instancia.fatiarListaEmPedacos = function (lista, numeroRegistroPorPagina) {
        var tamanhoListaFinal = lista.length;
        var listaContemElementos = tamanhoListaFinal != 0;
        var listaFatiada = [];
        var numeroPaginaAtual = Number(Instancia.elementoNumeroPaginaAtual.val());
        var indiceFatiaInicial = Instancia.getFatiaInicial(numeroPaginaAtual, numeroRegistroPorPagina);
        var indiceFatiaFinal = Instancia.getFatiaFinal(tamanhoListaFinal, indiceFatiaInicial, numeroRegistroPorPagina);
        var notPrimeiraPagina = numeroPaginaAtual != 1 || numeroRegistroPorPagina != 1;
        if (notPrimeiraPagina)
            indiceFatiaFinal++;
        while (listaContemElementos) {
            var fatia = this.getFatia(lista, indiceFatiaInicial, indiceFatiaFinal);
            listaFatiada.push(fatia);
            var concluiuPagina = listaFatiada.length == numeroRegistroPorPagina;
            if (concluiuPagina)
                break;
            else {
                listaContemElementos = tamanhoListaFinal >= indiceFatiaInicial + numeroRegistroPorPagina;
                indiceFatiaInicial += numeroRegistroPorPagina;
                indiceFatiaFinal += numeroRegistroPorPagina;
            }
        }
        return listaFatiada;
    };
    Instancia.getFatiaInicial = function (numeroPaginaAtual, tamanhoPorPedaco) {
        var ehPrimeiraPagina = numeroPaginaAtual == 1;
        if (ehPrimeiraPagina) {
            return 0;
        }
        else {
            var indicePaginaAtual = numeroPaginaAtual - 1;
            var indiceInicial = (indicePaginaAtual * tamanhoPorPedaco) - 1;
            var ehZero = indiceInicial == 0;
            if (ehZero)
                indiceInicial = 1;
            return indiceInicial;
        }
    };
    Instancia.getFatiaFinal = function (tamanhoLista, indiceFatiaInicial, numeroPorRegistro) {
        var indiceFatiaFinal = indiceFatiaInicial + numeroPorRegistro;
        var indiceFinalUltrapassado = indiceFatiaFinal > tamanhoLista - 1;
        if (indiceFinalUltrapassado) {
            indiceFatiaFinal = tamanhoLista - 1;
        }
        return indiceFatiaFinal;
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
            Instancia.atualizarPaginaExibida();
        });
        Instancia.getBtnNext().click(function () {
            isso.nextPagina();
        });
        Instancia.getSelectNumeroPaginaElemento().change(function (e) {
            Instancia.elementoNumeroPaginaAtual.val(1);
            Instancia.atualizarPaginaExibida();
        });
    };
    IndexController.prototype.nextPagina = function () {
        var numeroPaginaAtual = Number(Instancia.elementoNumeroPaginaAtual.val());
        var possivelPaginar = numeroPaginaAtual < Instancia.getNumeroPossivelPaginas();
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