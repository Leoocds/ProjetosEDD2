#include <iostream>
#include <iomanip>

using namespace std;

const int NUM_FILEIRAS = 15;
const int POLTRONAS_POR_FILEIRA = 40;

char mapaOcupacao[NUM_FILEIRAS][POLTRONAS_POR_FILEIRA];
int lugaresOcupados = 0;
double faturamentoTotal = 0.0;

void inicializarMapa() {
    for (int i = 0; i < NUM_FILEIRAS; ++i) {
        for (int j = 0; j < POLTRONAS_POR_FILEIRA; ++j) {
            mapaOcupacao[i][j] = '.';
        }
    }
}

void exibirMapa() {
    cout << "Mapa de ocupacao do teatro:" << endl;
    for (int i = 0; i < NUM_FILEIRAS; ++i) {
        for (int j = 0; j < POLTRONAS_POR_FILEIRA; ++j) {
            cout << mapaOcupacao[i][j];
        }
        cout << endl;
    }
}

void reservarPoltrona() {
    int fileira, poltrona;
    cout << "Digite o numero da fileira (1 a 15): ";
    cin >> fileira;
    cout << "Digite o numero da poltrona (1 a 40): ";
    cin >> poltrona;

    if (fileira < 1 || fileira > NUM_FILEIRAS || poltrona < 1 || poltrona > POLTRONAS_POR_FILEIRA) {
        cout << "Fileira ou poltrona invalida!" << endl;
        return;
    }

    if (mapaOcupacao[fileira - 1][poltrona - 1] == '#') {
        cout << "Esta poltrona ja esta ocupada!" << endl;
    } else {
        mapaOcupacao[fileira - 1][poltrona - 1] = '#';
        if (fileira <= 5) {
            faturamentoTotal += 50.00;
        } else if (fileira <= 10) {
            faturamentoTotal += 30.00;
        } else {
            faturamentoTotal += 15.00;
        }
        lugaresOcupados++;
        cout << "Poltrona reservada com sucesso!" << endl;
    }
}

void exibirFaturamento() {
    cout << "Quantidade de lugares ocupados: " << lugaresOcupados << endl;
    cout << "Valor da bilheteria: R$ " << faturamentoTotal << endl;
}

int main() {
    int opcao;
    inicializarMapa();

    do {
        cout << "Selecione uma opcao:" << endl;
        cout << "0. Finalizar" << endl;
        cout << "1. Reservar poltrona" << endl;
        cout << "2. Mapa de ocupacao" << endl;
        cout << "3. Faturamento" << endl;
        cin >> opcao;

        switch (opcao) {
            case 0:
                cout << "Programa finalizado." << endl;
                break;
            case 1:
                reservarPoltrona();
                break;
            case 2:
                exibirMapa();
                break;
            case 3:
                exibirFaturamento();
                break;
            default:
                cout << "Opcao invalida! Tente novamente." << endl;
        }
    } while (opcao != 0);

    return 0;
}
