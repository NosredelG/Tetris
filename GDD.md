# 🎮 Game Design Document (GDD) - Tetris Clone

---

## 1. Introdução

- **Título do Jogo:** Tetris Clone  
- **Descrição:** Um jogo de puzzle clássico onde o jogador organiza peças de formatos variados (Tetraminós) para formar linhas completas e eliminá-las da grade.  
  O objetivo é alcançar a maior pontuação possível antes que as peças empilhem até o topo.  
- **Plataforma:** PC (Windows/Mac/Linux)  
- **Público-Alvo:** Todas as idades  

---

## 2. Mecânicas de Jogo

### 2.1 Objetivo Principal

- Completar linhas horizontais com peças para eliminá-las e ganhar pontos.  
- Evitar que as peças empilhem até o topo da grade.

### 2.2 Gameplay

- **Tetraminós:**  
  Peças compostas por 4 blocos em diferentes formas:
  - I, O, T, S, Z, J, L  
  - São rotacionáveis em 90° no sentido horário.

- **Grade:**  
  - Tamanho: 10 colunas x 20 linhas.

- **Movimentos:**  
  - Setas esquerda/direita: Move a peça horizontalmente.  
  - Seta para baixo: Faz a peça cair mais rápido.  
  - Tecla para rotação: Rotaciona a peça.

- **Colisão:**  
  As peças param ao tocar:
  - Outras peças já posicionadas.  
  - O fundo da grade.

- **Linhas Completas:**  
  - Linhas horizontais completas desaparecem.  
  - As linhas acima descem para ocupar o espaço.

- **Game Over:**  
  - O jogo termina se qualquer peça alcançar o topo da grade.

---

## 3. Pontuação

### 3.1 Sistema de Pontos

- 1 linha eliminada: 100 pontos  
- 2 linhas eliminadas simultaneamente: 300 pontos  
- 3 linhas eliminadas simultaneamente: 600 pontos  
- 4 linhas eliminadas simultaneamente (Tetris): 1000 pontos

### 3.2 Multiplicador de Nível

- A cada 10 linhas eliminadas, o nível aumenta.  
- Pontuação é multiplicada pelo nível atual.

---

## 4. Visual e Áudio

### 4.1 Interface Visual

- **Grade:** Linhas visíveis que definem o campo de jogo.  
- **Peças:** Cores distintas para cada tipo de Tetraminó:
  - I: Azul-claro  
  - O: Amarelo  
  - T: Roxo  
  - S: Verde  
  - Z: Vermelho  
  - J: Azul-escuro  
  - L: Laranja  

- **HUD:**  
  - Pontuação atual  
  - Nível atual  
  - Próxima peça

### 4.2 Efeitos Visuais

- Partículas ao eliminar linhas.

### 4.3 Áudio

- Música de fundo: Tema clássico  
- Efeitos sonoros:
  - Parar peças  
  - Eliminar linhas

---

## 5. Controles

- ← / → : Move a peça para os lados  
- ↓ : Acelera a queda da peça  
- Espaço: Rotaciona a peça (ou outro botão definido)  
- P: Pausa o jogo

---

## 6. Dificuldade

- A velocidade de queda das peças aumenta com o progresso do jogador.  
- O intervalo inicial de queda é de 1 segundo e diminui conforme o nível sobe.

---

## 7. Funcionalidades Extras

- Uma peça translúcida mostra onde o Tetraminó atual irá cair (sombra da peça).

---

## 8. Desenvolvimento

### 8.1 Ferramentas

- **Engine:** Unity 2D  
- **Linguagem:** C#

### 8.2 Estrutura do Código

- **Scripts principais:**
  - `GameControl`: Gerencia o estado do jogo  
  - `InputManager`: Gerencia os controles do jogo  
  - `PieceMove`: Controla os movimentos e rotações das peças  
  - `Ghost`: Mostra a sombra da peça  
  - `SpawnTetro`: Gerencia a posição inicial e respawn das peças  
  - `AudioController`: Gerencia os sons do jogo

### 8.3 Fases de Desenvolvimento

1. **Protótipo inicial:**  
   Movimentos básicos e colisão  

2. **Implementação de mecânicas:**  
   Linhas completas, pontuação e níveis  

3. **Aprimoramentos:**  
   Sombra e HUD  

4. **Testes e ajustes:**  
   Ajustar dificuldade e corrigir bugs

---

## 9. Referências

- Jogo Tetris original da Nintendo  
- Mecânicas inspiradas nas versões modernas do Tetris  
