import React, { useState } from 'react';
import styled from 'styled-components';
import Player from './Player';
import BattleArea from './BattleArea';
import { GameState } from '../types/gameTypes';

const GameContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 2rem;
`;

const Game: React.FC = () => {
  const [gameState, setGameState] = useState<GameState>({
    playerHealth: 100,
    opponentHealth: 100,
    currentTurn: 'player',
    gameOver: false
  });

  const handleAttack = (damage: number) => {
    if (gameState.gameOver) return;

    if (gameState.currentTurn === 'player') {
      const newOpponentHealth = Math.max(0, gameState.opponentHealth - damage);
      setGameState(prev => ({
        ...prev,
        opponentHealth: newOpponentHealth,
        currentTurn: 'opponent',
        gameOver: newOpponentHealth === 0
      }));

      if (newOpponentHealth > 0) {
        setTimeout(() => {
          const opponentDamage = Math.floor(Math.random() * 30) + 10;
          const newPlayerHealth = Math.max(0, gameState.playerHealth - opponentDamage);
          setGameState(prev => ({
            ...prev,
            playerHealth: newPlayerHealth,
            currentTurn: 'player',
            gameOver: newPlayerHealth === 0
          }));
        }, 1500);
      }
    }
  };

  return (
    <GameContainer>
      <BattleArea
        playerHealth={gameState.playerHealth}
        opponentHealth={gameState.opponentHealth}
        currentTurn={gameState.currentTurn}
        gameOver={gameState.gameOver}
      />
      <Player onAttack={handleAttack} disabled={gameState.currentTurn !== 'player' || gameState.gameOver} />
    </GameContainer>
  );
};

export default Game;