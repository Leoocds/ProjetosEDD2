#include <iostream>
#include <queue>
#include <list>

using namespace std;

struct Guiche {
    int id;
    queue<int> senhasAtendidas;
};

void mostrarMenu() {
    cout << "Selecione uma opção:\n";
    cout << "0. Sair\n";
    cout << "1. Gerar senha\n";
    cout << "2. Abrir guichê\n";
    cout << "3. Realizar atendimento\n";
    cout << "4. Listar senhas atendidas por guichê\n";
}

void gerarSenha(queue<int>& senhasGeradas, int& senhaAtual) {
    senhasGeradas.push(senhaAtual);
    cout << "Senha gerada: " << senhaAtual << endl;
    senhaAtual++;
}

void abrirGuiche(list<Guiche>& guiches, int& guicheId) {
    Guiche novoGuiche;
    novoGuiche.id = guicheId++;
    guiches.push_back(novoGuiche);
    cout << "Guichê " << novoGuiche.id << " aberto." << endl;
}

void realizarAtendimento(queue<int>& senhasGeradas, list<Guiche>& guiches) {
    if (senhasGeradas.empty()) {
        cout << "Não há senhas aguardando atendimento." << endl;
        return;
    }

    int guicheId;
    cout << "Digite o id do guichê para realizar o atendimento: ";
    cin >> guicheId;

    auto it = guiches.begin();
    while (it != guiches.end() && it->id != guicheId) {
        ++it;
    }

    if (it != guiches.end()) {
        int senhaAtendida = senhasGeradas.front();
        senhasGeradas.pop();
        it->senhasAtendidas.push(senhaAtendida);
        cout << "Senha " << senhaAtendida << " atendida no guichê " << guicheId << "." << endl;
    } else {
        cout << "Guichê não encontrado." << endl;
    }
}

void listarSenhasAtendidas(const list<Guiche>& guiches) {
    int guicheId;
    cout << "Digite o id do guichê: ";
    cin >> guicheId;

    auto it = guiches.begin();
    while (it != guiches.end() && it->id != guicheId) {
        ++it;
    }

    if (it != guiches.end()) {
        cout << "Senhas atendidas pelo guichê " << guicheId << ": ";
        queue<int> senhas = it->senhasAtendidas;
        if (senhas.empty()) {
            cout << "Nenhuma senha foi atendida por este guichê." << endl;
        } else {
            while (!senhas.empty()) {
                cout << senhas.front() << " ";
                senhas.pop();
            }
            cout << endl;
        }
    } else {
        cout << "Guichê não encontrado." << endl;
    }
}

int main() {
    queue<int> senhasGeradas;
    list<Guiche> guiches;
    int opcao;
    int senhaAtual = 1;
    int guicheId = 1;

    do {
        cout << "\nSenhas aguardando atendimento: " << senhasGeradas.size() << endl;
        cout << "Guichês abertos: " << guiches.size() << endl;
        mostrarMenu();
        cin >> opcao;

        switch (opcao) {
            case 1:
                gerarSenha(senhasGeradas, senhaAtual);
                break;
            case 2:
                abrirGuiche(guiches, guicheId);
                break;
            case 3:
                realizarAtendimento(senhasGeradas, guiches);
                break;
            case 4:
                listarSenhasAtendidas(guiches);
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

    cout << "Programa encerrado. Total de senhas atendidas: " << senhaAtual - 1 - senhasGeradas.size() << endl;

    return 0;
}
