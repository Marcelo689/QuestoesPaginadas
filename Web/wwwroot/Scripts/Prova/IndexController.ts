class SelectPagina {
    public static opcoes : number[] = [
        1 ,2, 4 ,6 ,8 ,10
    ]; 
}
class QuestaoClass {
    public Id: number;
    public Descricao: string;
    public QuestaoDescricao1: string;
    public QuestaoDescricao2: string;
    public QuestaoDescricao3: string;
    public QuestaoDescricao4: string;
    public QuestaoDescricao5: string;
}
class EditarQuestao {

    public pegarQuestaoId() : number {
        var questaoOpcaoForName = $(".container-questao").last().find("label:first").attr("for");

        if (questaoOpcaoForName == undefined) {
            questaoOpcaoForName = "0_1opcao_1";
        }

        var stringId = questaoOpcaoForName.split("_")[1].toString().replace("opcao", "");
        var questaoId: number = Number(stringId);

        return questaoId;
    }

    public pegarProximaQuestaoId(): number {
        return this.pegarQuestaoId() + 1;
    }

    public removerIdDoNameTextArea(nameDefaultTextArea: string) {
        var indiceAntesDoId = nameDefaultTextArea.lastIndexOf("_");

        return nameDefaultTextArea.substr(0, indiceAntesDoId);
    }

    public atualizaNomesContendoId(esqueletoQuestao: string, idQuestao: number) {
        return esqueletoQuestao.replace("[id]", idQuestao.toString());
    }

    public gerarEsqueletoProximaQuestao(questaoClass: QuestaoClass): JQuery {

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
        var todosTextAreas: JQuery = jqueryEsqueletoNovo.find("textarea");

        todosTextAreas.each((indice, elemento) => {
            this.esvaziarTextArea(elemento);
        });

        function preencherDescricaoPrincipal() {
            jqueryEsqueletoNovo.find("li:first").text(questaoClass.Descricao);
        }

        return jqueryEsqueletoNovo;
    }
    public atualizarAtributosDescricaoPrincipalQuestao(elemento: HTMLElement, questaoId : number) {
        var containerQuestaoDiv = $(elemento).parents(".container-questao")[0];
        var inputDescricaoQuestaoPrincipal: JQuery = $($(containerQuestaoDiv).find("input")[0]);

        var nameAntigo = inputDescricaoQuestaoPrincipal.attr("name");
        var nameAtualizado = nameAntigo.replace("[id]", questaoId.toString());
        inputDescricaoQuestaoPrincipal.attr("name", nameAtualizado);

        var idAntigo = inputDescricaoQuestaoPrincipal.attr("id");
        var idAtualizado = idAntigo.replace("[id]", questaoId.toString());
        inputDescricaoQuestaoPrincipal.attr("id", idAtualizado)
    }
    public renomearIds(elemento: HTMLElement, IdDessaQuestao: number) {
        var questaoTemplate = `0_[id]opcao_1`;
        questaoTemplate = questaoTemplate.replace("[id]", IdDessaQuestao.toString());
        $(elemento).attr("id", questaoTemplate);
    }

    public esvaziarTextArea(elemento: HTMLElement) {
        $(elemento).text("");
    }

    private gerarNames(elemento: HTMLElement, IdDessaQuestao: number) {
        var nameDefaultTextArea = $(elemento).attr("name");
        var nomeDefaultSemId = this.removerIdDoNameTextArea(nameDefaultTextArea);
        var nameIdCorreto = nomeDefaultSemId + "_" + IdDessaQuestao;
        $(elemento).attr("name", nameIdCorreto);
    }
}

class BotoesAdicionarRemover {
    public static EditarQuestao = new EditarQuestao();

    public adicionarEventBotaoAdicionar() {

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
    private atualizarNumeroQuestoes() {
        var valorTextoNumeroQuestoes = Instancia.getElementoNumeroQuestoesAtual().text();
        var quantidadeItens: number = Number(valorTextoNumeroQuestoes);
        var quantidadeAtualizada: number = quantidadeItens + 1;
        Instancia.getElementoNumeroQuestoesAtual().text(quantidadeAtualizada);
        $("#QuantidadeQuestoesAtualizadas").val(quantidadeAtualizada);
    }

    public adicionarEventBotaoRemover() {
        $(".btn-remover").click(function () {

            var provaDeleteId = Number($(this).attr("data-id"));

            $.ajax({
                type: "post",
                data: { Id: provaDeleteId },
                url: "../../DeletarQuestao",
                success: function (dados) {
                    console.log(dados);    
                }
            });
        });
    }
}
class Instancia {
    static paginar() {
        var numeroPaginaAtual = Number(this.elementoNumeroPaginaAtual.val());
        var indiceSelectPagina = Number(this.elementoSelectNumeroPaginaAtual.val());
        var numeroRegistrosPorPagina = Number(SelectPagina.opcoes[indiceSelectPagina]);

        var indiceInicial = Instancia.getIndiceInicialPagina(numeroPaginaAtual, numeroRegistrosPorPagina);
        this.paginaExibidaElemento = this.elementoQuestoes.slice(indiceInicial, indiceInicial + numeroRegistrosPorPagina);
    }

    private static getIndiceInicialPagina(numeroPaginaAtual: number, numeroRegistrosPorPagina: number) : number {
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

    static elementoNumeroPaginaAtual: JQuery;
    static elementoNumeroQuestoesAtual: JQuery;
    static elementoQuestoes: JQuery;
    static btnPrevElement: JQuery<HTMLElement>;
    static btnNextElement: JQuery<HTMLElement>;
    static elementoSelectNumeroPaginaAtual: JQuery<HTMLElement>;
    static listaPaginadaElement: any;
    static paginaExibidaElemento: any;

    public static getElementoNumeroQuestoesAtual() {
        this.elementoNumeroQuestoesAtual = $("#itens-number");
        return this.elementoNumeroQuestoesAtual;
    }

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

    static atualizarPaginaExibida(isSelect: boolean = false) {
        Instancia.preeencherListaPaginada();
        this.ocultarTodasQuestoes();
        this.mostrarPaginaExibida(isSelect);
    }

    static mostrarPaginaExibida(isSelect : boolean) {
        var paginaExibida: any = Instancia.getPaginaExibidaElemento();
        if (isSelect)
            $(paginaExibida).show();
        else
            $(paginaExibida[0]).show();
    }

    static getNumeroPossivelPaginas() : number{
        var numeroTotalRegistros = this.elementoQuestoes.length;
        var indiceSelectNumeroPaginaAtual = Number(Instancia.getSelectNumeroPaginaElemento().val());
        var numeroRegistroPorPagina = SelectPagina.opcoes[indiceSelectNumeroPaginaAtual];

        var numeroTotalDePaginas = numeroTotalRegistros / numeroRegistroPorPagina;
        return numeroTotalDePaginas;
    }

    static ocultarTodasQuestoes() {
        var todasQuestoes : any = Instancia.getQuestoesElemento();
        todasQuestoes.hide();
    }

    static preeencherListaPaginada() {
        var elementoNumeroQuestoesPorPagina = Instancia.getSelectNumeroPaginaElemento();
        var elementoQuestoes = Instancia.getQuestoesElemento();

        var indiceSelectNumeroPaginaValor = Number(elementoNumeroQuestoesPorPagina.val());
        var numeroPorPagina: number = SelectPagina.opcoes[indiceSelectNumeroPaginaValor];

        Instancia.paginaExibidaElemento = Instancia.fatiarListaEmPedacos(elementoQuestoes, numeroPorPagina);
    }

    public static getIndiceFatiaFromPaginaAtual(numeroPaginaAtual: number) {
        var dados = numeroPaginaAtual - 1;

        const dadosZerado = dados == 0;
        if (dadosZerado) {
            dados = 1;
        }

        return dados;
    }

    private static getFatia(lista, indiceFatiaInicio, numeroRegistroPorPagina) {
        var fatia;
        if (indiceFatiaInicio == numeroRegistroPorPagina) {
            fatia = lista.slice(indiceFatiaInicio, numeroRegistroPorPagina + 1);
        } else {
            fatia = lista.slice(indiceFatiaInicio, numeroRegistroPorPagina);
        }

        return fatia;
    }

    static fatiarListaEmPedacos(lista: any, numeroRegistroPorPagina: number) {

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

    static getFatiaInicial(numeroPaginaAtual: number, tamanhoPorPedaco: number): number {
        const ehPrimeiraPagina = numeroPaginaAtual == 1;

        if (ehPrimeiraPagina) {
            return 0;
        } else {
            var indicePaginaAtual = numeroPaginaAtual - 1;
            var indiceInicial = (indicePaginaAtual * tamanhoPorPedaco);

            const ehZero = indiceInicial == 0;
            if (ehZero)
                indiceInicial = 1;
            return indiceInicial;
        }
    }

    static getFatiaFinal(tamanhoLista: number, indiceFatiaInicial: number, numeroPorRegistro: number): number {
        var indiceFatiaFinal = indiceFatiaInicial + numeroPorRegistro -1;
        const indiceFinalUltrapassado = indiceFatiaFinal > tamanhoLista - 1;

        if (indiceFinalUltrapassado) {
            indiceFatiaFinal = tamanhoLista - 1;    
        }

        return indiceFatiaFinal;
    }
}

class IndexController {

    public onLoad() {
        this.instanciarObjetosPagina();
        this.adicionarEventosObjetosPagina();
        BotoesAdicionarRemover.prototype.adicionarEventBotaoAdicionar();
        BotoesAdicionarRemover.prototype.adicionarEventBotaoRemover();
    }

    public adicionarEventosObjetosPagina() {
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

    public nextPagina() {
        var numeroPaginaAtual = Number(Instancia.elementoNumeroPaginaAtual.val());
        const possivelPaginar = numeroPaginaAtual < Instancia.getNumeroPossivelPaginas();

        if (possivelPaginar) {
            var proximaPagina = numeroPaginaAtual + 1;
            Instancia.elementoNumeroPaginaAtual.val(proximaPagina);
        }

        Instancia.atualizarPaginaExibida();
    }

    public prevPagina() {
        var numeroPaginaAtual = Number(Instancia.elementoNumeroPaginaAtual.val());
        const possivelPaginar = numeroPaginaAtual > 1;

        if (possivelPaginar) {
            var prevPagina = numeroPaginaAtual -1;
            Instancia.elementoNumeroPaginaAtual.val(prevPagina);
        }

        Instancia.atualizarPaginaExibida();
    }

    public instanciarObjetosPagina() {
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



