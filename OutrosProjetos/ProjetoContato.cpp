#include <iostream>
#include <string>
using namespace std;

class Data {
private:
    int dia;
    int mes;
    int ano;

public:
    Data(int dia, int mes, int ano) {
        this->dia = dia;
        this->mes = mes;
        this->ano = ano;
    }
    Data() {
        this->dia = 0;
        this->mes = 0;
        this->ano = 0;
    }
    void setDia(int dia) {
        this->dia = dia;
    }
    void setMes(int mes) {
        this->mes = mes;
    }
    void setAno(int ano) {
        this->ano = ano;
    }
    int getDia() {
        return this->dia;
    }
    int getMes() {
        return this->mes;
    }
    int getAno() {
        return this->ano;
    }
    string getData() {
        string sDia = to_string(this->dia);
        string sMes = to_string(this->mes);
        string sAno = to_string(this->ano);

        return sDia.insert(0, 2 - sDia.size(), '0') + "/" +
               sMes.insert(0, 2 - sMes.size(), '0') + "/" +
               sAno;
    }
};

class Contato {
private:
    string email;
    string nome;
    string telefone;
    Data dtnasc;

public:
    Contato(string email, string nome, string telefone, Data dtnasc) {
        this->email = email;
        this->nome = nome;
        this->telefone = telefone;
        this->dtnasc = dtnasc;
    }
    Contato() {
        this->email = "";
        this->nome = "";
        this->telefone = "";
    }
    void setEmail(string email) {
        this->email = email;
    }
    void setNome(string nome) {
        this->nome = nome;
    }
    void setTelefone(string telefone) {
        this->telefone = telefone;
    }
    void setDataNascimento(Data dtnasc) {
        this->dtnasc = dtnasc;
    }
    string getEmail() {
        return this->email;
    }
    string getNome() {
        return this->nome;
    }
    string getTelefone() {
        return this->telefone;
    }
    Data getDataNascimento() {
        return this->dtnasc;
    }
    int idade() {
        int anoAtual = 2024;
        return anoAtual - this->dtnasc.getAno();
    }
};

int main() {
    Contato contatos[5];

    for (int i = 0; i < 5; i++) {
        cout << "Cadastro do Contato " << i+1 << endl;
        string email, nome, telefone;
        int dia, mes, ano;

        cout << "Email: ";
        getline(cin, email);
        cout << "Nome: ";
        getline(cin, nome);
        cout << "Telefone: ";
        getline(cin, telefone);
        cout << "Data de Nascimento (DD MM AAAA): ";
        cin >> dia >> mes >> ano;
      
        Data dataNascimento(dia, mes, ano);

        contatos[i] = Contato(email, nome, telefone, dataNascimento);
    }

    cout << "\nExibindo contatos:\n";
    for (int i = 0; i < 5; i++) {
        cout << "Contato " << i+1 << ":" << endl;
        cout << "Nome: " << contatos[i].getNome() << endl;
        cout << "Email: " << contatos[i].getEmail() << endl;
        cout << "Telefone: " << contatos[i].getTelefone() << endl;
        cout << "Idade: " << contatos[i].idade() << " anos" << endl;
        cout << endl;
    }

    return 0;
}
