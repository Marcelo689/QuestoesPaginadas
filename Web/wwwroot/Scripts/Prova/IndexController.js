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
var IndexController = /** @class */ (function () {
    function IndexController() {
    }
    IndexController.prototype.getTotalPaginas = function () {
        var quantidadeQuestoes = this.elementoQuestoes.length;
        var valueSelectQuestoesPorPagina = Number(this.elementoNumeroQuestoesPorPagina.val());
        var numeroPaginas = SelectPagina.opcoes[valueSelectQuestoesPorPagina];
        var numeroTotalPaginas = quantidadeQuestoes / numeroPaginas;
        return numeroTotalPaginas;
    };
    IndexController.prototype.proximaPagina = function () {
        var numeroPaginaAtual = Number(this.elementoNumeroPaginaAtual.val());
        var numeroTotalPaginas = this.getTotalPaginas();
        var paginaEhValidaParaAvancar = numeroPaginaAtual < numeroTotalPaginas;
        var proximaPagina = numeroPaginaAtual + 1;
        this.elementoNumeroPaginaAtual.val(proximaPagina);
    };
    IndexController.prototype.voltarPagina = function () {
        var numeroPaginaAtual = Number(this.elementoNumeroPaginaAtual.val());
        var paginaEhValidaParaVoltar = numeroPaginaAtual > 2;
        if (paginaEhValidaParaVoltar) {
            var proximaPagina = numeroPaginaAtual + 1;
            this.elementoNumeroPaginaAtual.val(proximaPagina);
        }
    };
    IndexController.prototype.instanciaSelectNumeroPaginaElemento = function () {
        this.elementoNumeroPaginaAtual = $("#numeroQuestaoPorPagina");
    };
    IndexController.prototype.instanciaQuestoesElemento = function () {
        this.elementoQuestoes = $(".container-questao");
    };
    IndexController.prototype.onLoad = function () {
        this.instanciaSelectNumeroPaginaElemento();
        this.instanciaQuestoesElemento();
        this.instanciaNumeroPaginaElemento();
    };
    IndexController.prototype.instanciaNumeroPaginaElemento = function () {
        this.elementoNumeroPaginaAtual = $("#pagina-atual");
    };
    return IndexController;
}());
var controller = new IndexController();
console.log("chegou no arquivo Ts");
//# sourceMappingURL=IndexController.js.map