class SelectPagina {
}
SelectPagina.opcoes = [
    1, 2, 4, 6, 8, 10
];
class QuestaoClass {
}
class EditarQuestao {
    pegarQuestaoId() {
        var questaoOpcaoForName = $(".container-questao").last().find("label:first").attr("for");
        var stringId = questaoOpcaoForName.split("_")[1].toString().replace("opcao", "");
        var questaoId = Number(stringId);
        return questaoId;
    }
    pegarProximaQuestaoId() {
        return this.pegarQuestaoId() + 1;
    }
    removerIdDoNameTextArea(nameDefaultTextArea) {
        var indiceAntesDoId = nameDefaultTextArea.lastIndexOf("_");
        return nameDefaultTextArea.substr(0, indiceAntesDoId);
    }
    atualizaNomesContendoId(esqueletoQuestao, idQuestao) {
        return esqueletoQuestao.replace("[id]", idQuestao.toString());
    }
    gerarEsqueletoProximaQuestao(questaoClass) {
        var IdDessaQuestao = this.pegarProximaQuestaoId();
        var jqueryEsqueleto = $(`
                    <div class="container-questao" style="display: block;">
                        <li>
                            <input class="form-control" type="text" name="descricao_[id]" id="descricao_[id]"/>
                        </li>
                    <style>
                        .label-opcao {
                            margin-left: 10px;
                            width: 100%;
                        }
                    </style>
                    <div class="container-opcoes d-flex flex-column w-75">
                            <div class="w-75 d-flex justify-content-between text-lg-start">
                                <label for="0_2opcao_1" class="label-opcao">
                                    <textarea name="opcao_descricao_2_1" id="0_2opcao_1">44</textarea>
                                </label>
                            </div>
                            <div class="w-75 d-flex justify-content-between text-lg-start">
                                <label for="1_2opcao_2" class="label-opcao">
                                    <textarea name="opcao_descricao_2_2" id="1_2opcao_2">11</textarea>
                                </label>
                            </div>
                            <div class="w-75 d-flex justify-content-between text-lg-start">
                                <label for="2_2opcao_3" class="label-opcao">
                                    <textarea name="opcao_descricao_2_3" id="2_2opcao_3">3</textarea>
                                </label>
                            </div>
                            <div class="w-75 d-flex justify-content-between text-lg-start">
                                <label for="3_2opcao_4" class="label-opcao">
                                    <textarea name="opcao_descricao_2_4" id="3_2opcao_4">4</textarea>
                                </label>
                            </div>
                            <div class="w-75 d-flex justify-content-between text-lg-start">
                                <label for="4_2opcao_5" class="label-opcao">
                                    <textarea name="opcao_descricao_2_5" id="4_2opcao_5">5</textarea>
                                </label>
                            </div>
                    </div>
                            </div>
        `);
        var textoQuestao = `
                    <div class="container-questao" style="display: block;">
                        <li>
                            <input class="form-control" type="text" name="descricao_[id]" id="descricao_[id]"/>
                        </li>
                    <style>
                        .label-opcao {
                            margin-left: 10px;
                            width: 100%;
                        }
                    </style>
                    <div class="container-opcoes d-flex flex-column w-75">
                            <div class="w-75 d-flex justify-content-between text-lg-start">
                                <label for="0_[id]opcao_1" class="label-opcao">
                                    <textarea name="opcao_descricao_[id]_1" id="0_[id]opcao_1">44</textarea>
                                </label>
                            </div>
                            <div class="w-75 d-flex justify-content-between text-lg-start">
                                <label for="1_[id]opcao_2" class="label-opcao">
                                    <textarea name="opcao_descricao_[id]_2" id="1_[id]opcao_2">11</textarea>
                                </label>
                            </div>
                            <div class="w-75 d-flex justify-content-between text-lg-start">
                                <label for="2_[id]opcao_3" class="label-opcao">
                                    <textarea name="opcao_descricao_[id]_3" id="2_[id]opcao_3">3</textarea>
                                </label>
                            </div>
                            <div class="w-75 d-flex justify-content-between text-lg-start">
                                <label for="3_[id]opcao_4" class="label-opcao">
                                    <textarea name="opcao_descricao_[id]_4" id="3_[id]opcao_4">4</textarea>
                                </label>
                            </div>
                            <div class="w-75 d-flex justify-content-between text-lg-start">
                                <label for="4_[id]opcao_5" class="label-opcao">
                                    <textarea name="opcao_descricao_[id]_5" id="4_[id]opcao_5">5</textarea>
                                </label>
                            </div>
                    </div>
                            </div>
        `;
        var jqueryEsqueletoNovo = $(textoQuestao.replaceAll("[id]", IdDessaQuestao.toString()));
        preencherDescricaoPrincipal();
        var todosTextAreas = jqueryEsqueletoNovo.find("textarea");
        todosTextAreas.each((indice, elemento) => {
            this.esvaziarTextArea(elemento);
        });
        function preencherDescricaoPrincipal() {
            jqueryEsqueletoNovo.find("li:first").text(questaoClass.Descricao);
        }
        return jqueryEsqueletoNovo;
    }
    atualizarAtributosDescricaoPrincipalQuestao(elemento, questaoId) {
        var containerQuestaoDiv = $(elemento).parents(".container-questao")[0];
        var inputDescricaoQuestaoPrincipal = $($(containerQuestaoDiv).find("input")[0]);
        var nameAntigo = inputDescricaoQuestaoPrincipal.attr("name");
        var nameAtualizado = nameAntigo.replace("[id]", questaoId.toString());
        inputDescricaoQuestaoPrincipal.attr("name", nameAtualizado);
        var idAntigo = inputDescricaoQuestaoPrincipal.attr("id");
        var idAtualizado = idAntigo.replace("[id]", questaoId.toString());
        inputDescricaoQuestaoPrincipal.attr("id", idAtualizado);
    }
    renomearIds(elemento, IdDessaQuestao) {
        var questaoTemplate = `0_[id]opcao_1`;
        questaoTemplate = questaoTemplate.replace("[id]", IdDessaQuestao.toString());
        $(elemento).attr("id", questaoTemplate);
    }
    esvaziarTextArea(elemento) {
        $(elemento).text("");
    }
    gerarNames(elemento, IdDessaQuestao) {
        var nameDefaultTextArea = $(elemento).attr("name");
        var nomeDefaultSemId = this.removerIdDoNameTextArea(nameDefaultTextArea);
        var nameIdCorreto = nomeDefaultSemId + "_" + IdDessaQuestao;
        $(elemento).attr("name", nameIdCorreto);
    }
}
class BotoesAdicionarRemover {
    adicionarEventBotaoAdicionar() {
        var isso = this;
        var containerListaQuestoes = $("#container-lista-questoes");
        $(".btn-adicionar").click(function () {
            var questaoData = new QuestaoClass();
            var questaoNovaHtml = EditarQuestao.prototype.gerarEsqueletoProximaQuestao(questaoData);
            containerListaQuestoes.prepend(questaoNovaHtml[0]);
            isso.atualizarNumeroQuestoes();
            Instancia.atualizarPaginaExibida();
        });
    }
    atualizarNumeroQuestoes() {
        var valorTextoNumeroQuestoes = Instancia.getElementoNumeroQuestoesAtual().text();
        var quantidadeItens = Number(valorTextoNumeroQuestoes);
        var quantidadeAtualizada = quantidadeItens + 1;
        Instancia.getElementoNumeroQuestoesAtual().text(quantidadeAtualizada);
        $("#QuantidadeQuestoesAtualizadas").val(quantidadeAtualizada);
    }
    adicionarEventBotaoRemover() {
        $(".btn-remover").click(function () {
        });
    }
}
BotoesAdicionarRemover.EditarQuestao = new EditarQuestao();
class Instancia {
    static paginar() {
        var numeroPaginaAtual = Number(this.elementoNumeroPaginaAtual.val());
        var indiceSelectPagina = Number(this.elementoSelectNumeroPaginaAtual.val());
        var numeroRegistrosPorPagina = Number(SelectPagina.opcoes[indiceSelectPagina]);
        var indiceInicial = Instancia.getIndiceInicialPagina(numeroPaginaAtual, numeroRegistrosPorPagina);
        this.paginaExibidaElemento = this.elementoQuestoes.slice(indiceInicial, indiceInicial + numeroRegistrosPorPagina);
    }
    static getIndiceInicialPagina(numeroPaginaAtual, numeroRegistrosPorPagina) {
        var indiceInicial = 0;
        const primeiraPagina = numeroPaginaAtual != 1;
        if (primeiraPagina) {
            indiceInicial = numeroPaginaAtual * numeroRegistrosPorPagina;
        }
        return indiceInicial;
    }
    static listaPaginadaElemento() {
        this.preeencherListaPaginada();
    }
    static getPaginaExibidaElemento() {
        return this.paginaExibidaElemento;
    }
    static getElementoNumeroQuestoesAtual() {
        this.elementoNumeroQuestoesAtual = $("#itens-number");
        return this.elementoNumeroQuestoesAtual;
    }
    static selectNumeroPaginaElemento() {
        this.elementoSelectNumeroPaginaAtual = $("#numeroQuestaoPorPagina");
    }
    static getSelectNumeroPaginaElemento() {
        return this.elementoSelectNumeroPaginaAtual;
    }
    static questoesElemento() {
        this.elementoQuestoes = $(".container-questao");
    }
    static getQuestoesElemento() {
        return this.elementoQuestoes;
    }
    static BtnNext() {
        this.btnNextElement = $("#btn-next");
    }
    static getBtnNext() {
        return this.btnNextElement;
    }
    static BtnPrev() {
        this.btnPrevElement = $("#btn-prev");
    }
    static getBtnPrev() {
        return this.btnPrevElement;
    }
    static numeroPaginaElemento() {
        this.elementoNumeroPaginaAtual = $("#pagina-atual");
    }
    static getNumeroPaginaElemento() {
        return this.elementoNumeroPaginaAtual;
    }
    static atualizarPaginaExibida(isSelect = false) {
        Instancia.preeencherListaPaginada();
        this.ocultarTodasQuestoes();
        this.mostrarPaginaExibida(isSelect);
    }
    static mostrarPaginaExibida(isSelect) {
        var paginaExibida = Instancia.getPaginaExibidaElemento();
        if (isSelect)
            $(paginaExibida).show();
        else
            $(paginaExibida[0]).show();
    }
    static getNumeroPossivelPaginas() {
        var numeroTotalRegistros = this.elementoQuestoes.length;
        var indiceSelectNumeroPaginaAtual = Number(Instancia.getSelectNumeroPaginaElemento().val());
        var numeroRegistroPorPagina = SelectPagina.opcoes[indiceSelectNumeroPaginaAtual];
        var numeroTotalDePaginas = numeroTotalRegistros / numeroRegistroPorPagina;
        return numeroTotalDePaginas;
    }
    static ocultarTodasQuestoes() {
        var todasQuestoes = Instancia.getQuestoesElemento();
        todasQuestoes.hide();
    }
    static preeencherListaPaginada() {
        var elementoNumeroQuestoesPorPagina = Instancia.getSelectNumeroPaginaElemento();
        var elementoQuestoes = Instancia.getQuestoesElemento();
        var indiceSelectNumeroPaginaValor = Number(elementoNumeroQuestoesPorPagina.val());
        var numeroPorPagina = SelectPagina.opcoes[indiceSelectNumeroPaginaValor];
        Instancia.paginaExibidaElemento = Instancia.fatiarListaEmPedacos(elementoQuestoes, numeroPorPagina);
    }
    static getIndiceFatiaFromPaginaAtual(numeroPaginaAtual) {
        var dados = numeroPaginaAtual - 1;
        const dadosZerado = dados == 0;
        if (dadosZerado) {
            dados = 1;
        }
        return dados;
    }
    static getFatia(lista, indiceFatiaInicio, numeroRegistroPorPagina) {
        var fatia;
        if (indiceFatiaInicio == numeroRegistroPorPagina) {
            fatia = lista.slice(indiceFatiaInicio, numeroRegistroPorPagina + 1);
        }
        else {
            fatia = lista.slice(indiceFatiaInicio, numeroRegistroPorPagina);
        }
        return fatia;
    }
    static fatiarListaEmPedacos(lista, numeroRegistroPorPagina) {
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
            const concluiuPagina = listaFatiada.length == numeroRegistroPorPagina;
            if (concluiuPagina)
                break;
            else {
                listaContemElementos = tamanhoListaFinal >= indiceFatiaInicial + numeroRegistroPorPagina;
                indiceFatiaInicial += numeroRegistroPorPagina;
                indiceFatiaFinal += numeroRegistroPorPagina;
            }
        }
        return listaFatiada;
    }
    static getFatiaInicial(numeroPaginaAtual, tamanhoPorPedaco) {
        const ehPrimeiraPagina = numeroPaginaAtual == 1;
        if (ehPrimeiraPagina) {
            return 0;
        }
        else {
            var indicePaginaAtual = numeroPaginaAtual - 1;
            var indiceInicial = (indicePaginaAtual * tamanhoPorPedaco);
            const ehZero = indiceInicial == 0;
            if (ehZero)
                indiceInicial = 1;
            return indiceInicial;
        }
    }
    static getFatiaFinal(tamanhoLista, indiceFatiaInicial, numeroPorRegistro) {
        var indiceFatiaFinal = indiceFatiaInicial + numeroPorRegistro - 1;
        const indiceFinalUltrapassado = indiceFatiaFinal > tamanhoLista - 1;
        if (indiceFinalUltrapassado) {
            indiceFatiaFinal = tamanhoLista - 1;
        }
        return indiceFatiaFinal;
    }
}
class IndexController {
    onLoad() {
        this.instanciarObjetosPagina();
        this.adicionarEventosObjetosPagina();
        BotoesAdicionarRemover.prototype.adicionarEventBotaoAdicionar();
        BotoesAdicionarRemover.prototype.adicionarEventBotaoRemover();
    }
    adicionarEventosObjetosPagina() {
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
    }
    nextPagina() {
        var numeroPaginaAtual = Number(Instancia.elementoNumeroPaginaAtual.val());
        const possivelPaginar = numeroPaginaAtual < Instancia.getNumeroPossivelPaginas();
        if (possivelPaginar) {
            var proximaPagina = numeroPaginaAtual + 1;
            Instancia.elementoNumeroPaginaAtual.val(proximaPagina);
        }
        Instancia.atualizarPaginaExibida();
    }
    prevPagina() {
        var numeroPaginaAtual = Number(Instancia.elementoNumeroPaginaAtual.val());
        const possivelPaginar = numeroPaginaAtual > 1;
        if (possivelPaginar) {
            var prevPagina = numeroPaginaAtual - 1;
            Instancia.elementoNumeroPaginaAtual.val(prevPagina);
        }
        Instancia.atualizarPaginaExibida();
    }
    instanciarObjetosPagina() {
        Instancia.questoesElemento();
        Instancia.selectNumeroPaginaElemento();
        Instancia.numeroPaginaElemento();
        Instancia.listaPaginadaElemento();
        Instancia.atualizarPaginaExibida();
        Instancia.BtnPrev();
        Instancia.BtnNext();
    }
}
var controller = new IndexController();
document.addEventListener("DOMContentLoaded", function () {
    controller.onLoad();
});
//# sourceMappingURL=IndexController.js.map