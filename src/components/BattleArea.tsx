import React from 'react';
import styled from 'styled-components';
import HealthBar from './HealthBar';

const BattleContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 2rem;
  width: 100%;
  max-width: 800px;
`;

const BattleField = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 2rem;
  background: rgba(255, 255, 255, 0.1);
  border-radius: 16px;
`;

const Character = styled.div<{ isOpponent?: boolean }>`
  font-size: 4rem;
  transform: ${props => props.isOpponent ? 'scaleX(-1)' : 'none'};
`;

const StatusText = styled.div`
  text-align: center;
  font-size: 1.5rem;
  color: #ffd700;
`;

interface BattleAreaProps {
  playerHealth: number;
  opponentHealth: number;
  currentTurn: 'player' | 'opponent';
  gameOver: boolean;
}

const BattleArea: React.FC<BattleAreaProps> = ({
  playerHealth,
  opponentHealth,
  currentTurn,
  gameOver
}) => {
  return (
    <BattleContainer>
      <div>
        <h3>Opponent</h3>
        <HealthBar health={opponentHealth} />
      </div>
      <BattleField>
        <Character>🎤</Character>
        <Character isOpponent>🎤</Character>
      </BattleField>
      <div>
        <h3>Player</h3>
        <HealthBar health={playerHealth} />
      </div>
      <StatusText>
        {gameOver 
          ? `Game Over! ${playerHealth === 0 ? 'Opponent' : 'Player'} Wins!`
          : `${currentTurn === 'player' ? 'Your' : "Opponent's"} Turn`}
      </StatusText>
    </BattleContainer>
  );
};

export default BattleArea;