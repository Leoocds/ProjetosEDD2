// Leonardo de Lima Pedroso – CB3026655

#include <iostream>
#include <queue>

using namespace std;

void mostrarMenu() {
    cout << "Selecione uma opção:\n";
    cout << "0. Sair\n";
    cout << "1. Gerar senha\n";
    cout << "2. Realizar atendimento\n";
}

void gerarSenha(queue<int>& senhasGeradas, int& senhaAtual) {
    senhasGeradas.push(senhaAtual);
    cout << "Senha gerada: " << senhaAtual << endl;
    senhaAtual++;
}

void realizarAtendimento(queue<int>& senhasGeradas, queue<int>& senhasAtendidas) {
    if (!senhasGeradas.empty()) {
        int senhaAtendida = senhasGeradas.front();
        senhasGeradas.pop();
        senhasAtendidas.push(senhaAtendida);
        cout << "Senha atendida: " << senhaAtendida << endl;
    } else {
        cout << "Não há senhas aguardando atendimento." << endl;
    }
}

int main() {
    queue<int> senhasGeradas;
    queue<int> senhasAtendidas;
    int opcao;
    int senhaAtual = 1; 

    do {
        cout << "Senhas aguardando atendimento: " << senhasGeradas.size() << endl;
        mostrarMenu();
        cin >> opcao;

        switch (opcao) {
            case 1:
                gerarSenha(senhasGeradas, senhaAtual);
                break;
            case 2:
                realizarAtendimento(senhasGeradas, senhasAtendidas);
                break;
            case 0:
                if (!senhasGeradas.empty()) {
                    cout << "Ainda há senhas aguardando atendimento. Continue o atendimento." << endl;
                    opcao = -1; 
                }
                break;
            default:
                cout << "Opção inválida. Tente novamente." << endl;
        }
    } while (opcao != 0);

    cout << "Programa encerrado. Senhas atendidas: " << senhasAtendidas.size() << endl;

    return 0;
}




