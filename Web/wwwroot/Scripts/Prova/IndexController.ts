﻿class SelectPagina {
    public static opcoes : number[] = [
        1 ,2, 4 ,6 ,8 ,10
    ]; 
}

class Instancia {
    static listaPaginadaElemento() {
        this.preeencherListaPaginada();
    }

    static getPaginaExibidaElemento() {
        return this.paginaExibidaElemento;
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
        Instancia.preeencherListaPaginada();
        this.ocultarTodasQuestoes();
        this.mostrarPaginaExibida();
    }

    static mostrarPaginaExibida() {
        var paginaExibida: any = Instancia.getPaginaExibidaElemento();
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

        while (listaContemElementos) {

            const indiceFinalInvalido = indiceFatiaFinal > tamanhoListaFinal;
            if (indiceFinalInvalido) {
                break;
            }

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
            var indiceInicial = (indicePaginaAtual * tamanhoPorPedaco) - 1;
            return indiceInicial;
        }
    }

    static getFatiaFinal(tamanhoLista: number, indiceFatiaInicial: number, numeroPorRegistro: number): number {
        var indiceFatiaFinal = indiceFatiaInicial + numeroPorRegistro;
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
            Instancia.atualizarPaginaExibida();
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


