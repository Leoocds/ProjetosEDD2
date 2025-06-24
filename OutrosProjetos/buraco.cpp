#include <iostream>
#include <cstdlib>
#include <ctime>
#include <vector>
#include <algorithm>
#include <sstream>

void random_shuffle(std::vector<std::string>& vec) {
    for (int i = vec.size() - 1; i > 0; --i) {
        int j = std::rand() % (i + 1);
        std::swap(vec[i], vec[j]);
    }
}

std::string to_card_string(int num) {
    std::stringstream ss;
    ss << num;
    return ss.str();
}

int main() {
    std::srand(std::time(0));

    std::vector<std::vector<std::string>> baralho(2);
    for (int b = 0; b < 2; b++) {
        for (int n = 1; n <= 4; n++) {
            for (int c = 1; c <= 13; c++) {
                std::string carta = to_card_string(n) + "-" + (c < 10 ? "0" : "") + to_card_string(c) + "-" + to_card_string(b + 1);
                baralho[b].push_back(carta);
            }
        }
        random_shuffle(baralho[b]);
    }

    std::vector<std::vector<std::string>> mao(4);
    for (int j = 0; j < 4; j++) {
        for (int b = 0; b < 2; b++) {
            for (int c = 0; c < 11; c++) {
                mao[j].push_back(baralho[b].back());
                baralho[b].pop_back();
            }
        }
    }

    for (int j = 0; j < 4; j++) {
        std::cout << "Jogador " << (j + 1) << ":\n";
        for (int c = 0; c < 11; c++) {
            std::cout << "mao[" << (j) << "][" << (c) << "] = \"" << mao[j][c] << "\"\n";
        }
        std::cout << "\n";
    }

    return 0;
}
