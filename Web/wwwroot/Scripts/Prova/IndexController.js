var SelectPagina = /** @class */ (function () {
    function SelectPagina() {
    }
    SelectPagina.opcoes = [
        1, 2, 4, 6, 8, 10
    ];
    return SelectPagina;
}());
var QuestaoClass = /** @class */ (function () {
    function QuestaoClass() {
    }
    return QuestaoClass;
}());
var EditarQuestao = /** @class */ (function () {
    function EditarQuestao() {
    }
    EditarQuestao.prototype.pegarQuestaoId = function () {
        var questaoOpcaoForName = $(".container-questao").last().find("label:first").attr("for");
        var questaoId = Number(questaoOpcaoForName.split("_")[2]);
        return questaoId;
    };
    EditarQuestao.prototype.pegarProximaQuestaoId = function () {
        return this.pegarQuestaoId() + 1;
    };
    EditarQuestao.prototype.removerIdDoNameTextArea = function (nameDefaultTextArea) {
        var indiceAntesDoId = nameDefaultTextArea.lastIndexOf("_");
        return nameDefaultTextArea.substr(0, indiceAntesDoId);
    };
    EditarQuestao.prototype.gerarEsqueletoProximaQuestao = function (questaoClass) {
        var _this = this;
        var IdDessaQuestao = this.pegarProximaQuestaoId();
        var jqueryEsqueleto = $("\n                                <div class=\"container-questao\" style=\"display: none;\">\n                                <li>Quantos lados possui o triangulo</li>\n\n\n                    <style>\n                        .label-opcao {\n                            margin-left: 10px;\n                            width: 100%;\n                        }\n                    </style>\n\n                    <div class=\"container-opcoes d-flex flex-column w-75\">\n\n\n                            <div class=\"w-75 d-flex justify-content-between text-lg-start\">\n\n                                <label for=\"0_2opcao_1\" class=\"label-opcao\">\n                                    <textarea name=\"opcao_descricao_2_1\" id=\"0_2opcao_1\">44</textarea>\n                                </label>\n\n                            </div>\n                            <div class=\"w-75 d-flex justify-content-between text-lg-start\">\n\n                                <label for=\"1_2opcao_2\" class=\"label-opcao\">\n                                    <textarea name=\"opcao_descricao_2_2\" id=\"1_2opcao_2\">11</textarea>\n                                </label>\n\n                            </div>\n                            <div class=\"w-75 d-flex justify-content-between text-lg-start\">\n\n                                <label for=\"2_2opcao_3\" class=\"label-opcao\">\n                                    <textarea name=\"opcao_descricao_2_3\" id=\"2_2opcao_3\">3</textarea>\n                                </label>\n\n                            </div>\n                            <div class=\"w-75 d-flex justify-content-between text-lg-start\">\n\n                                <label for=\"3_2opcao_4\" class=\"label-opcao\">\n                                    <textarea name=\"opcao_descricao_2_4\" id=\"3_2opcao_4\">4</textarea>\n                                </label>\n\n                            </div>\n                            <div class=\"w-75 d-flex justify-content-between text-lg-start\">\n\n                                <label for=\"4_2opcao_5\" class=\"label-opcao\">\n                                    <textarea name=\"opcao_descricao_2_5\" id=\"4_2opcao_5\">5</textarea>\n                                </label>\n\n                            </div>\n                    </div>\n                            </div>\n        ");
        preencherDescricaoPrincipal();
        var todosTextAreas = jqueryEsqueleto.find("textarea");
        todosTextAreas.each(function (indice, elemento) {
            _this.gerarNames(elemento, IdDessaQuestao);
            _this.esvaziarTextArea(elemento);
        });
        function preencherDescricaoPrincipal() {
            jqueryEsqueleto.find("li:first").text(questaoClass.Descricao);
        }
        return jqueryEsqueleto;
    };
    EditarQuestao.prototype.esvaziarTextArea = function (elemento) {
        $(elemento).text("");
    };
    EditarQuestao.prototype.gerarNames = function (elemento, IdDessaQuestao) {
        var nameDefaultTextArea = $(elemento).attr("name");
        var nomeDefaultSemId = this.removerIdDoNameTextArea(nameDefaultTextArea);
        var nameIdCorreto = nomeDefaultSemId + IdDessaQuestao;
        $(elemento).attr("name", nameIdCorreto);
    };
    return EditarQuestao;
}());
var Instancia = /** @class */ (function () {
    function Instancia() {
    }
    Instancia.paginar = function () {
        var numeroPaginaAtual = Number(this.elementoNumeroPaginaAtual.val());
        var indiceSelectPagina = Number(this.elementoSelectNumeroPaginaAtual.val());
        var numeroRegistrosPorPagina = Number(SelectPagina.opcoes[indiceSelectPagina]);
        var indiceInicial = Instancia.getIndiceInicialPagina(numeroPaginaAtual, numeroRegistrosPorPagina);
        this.paginaExibidaElemento = this.elementoQuestoes.slice(indiceInicial, indiceInicial + numeroRegistrosPorPagina);
    };
    Instancia.getIndiceInicialPagina = function (numeroPaginaAtual, numeroRegistrosPorPagina) {
        var indiceInicial = 0;
        var primeiraPagina = numeroPaginaAtual != 1;
        if (primeiraPagina) {
            indiceInicial = numeroPaginaAtual * numeroRegistrosPorPagina;
        }
        return indiceInicial;
    };
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
    Instancia.atualizarPaginaExibida = function (isSelect) {
        if (isSelect === void 0) { isSelect = false; }
        Instancia.preeencherListaPaginada();
        this.ocultarTodasQuestoes();
        this.mostrarPaginaExibida(isSelect);
    };
    Instancia.mostrarPaginaExibida = function (isSelect) {
        var paginaExibida = Instancia.getPaginaExibidaElemento();
        if (isSelect)
            $(paginaExibida).show();
        else
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
            var indiceInicial = (indicePaginaAtual * tamanhoPorPedaco);
            var ehZero = indiceInicial == 0;
            if (ehZero)
                indiceInicial = 1;
            return indiceInicial;
        }
    };
    Instancia.getFatiaFinal = function (tamanhoLista, indiceFatiaInicial, numeroPorRegistro) {
        var indiceFatiaFinal = indiceFatiaInicial + numeroPorRegistro - 1;
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
            Instancia.paginar();
            Instancia.ocultarTodasQuestoes();
            Instancia.mostrarPaginaExibida(true);
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