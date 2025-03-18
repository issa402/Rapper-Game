import React from 'react';
import styled from 'styled-components';
import { attacks } from '../data/attacks';

const PlayerContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 1rem;
  width: 100%;
  max-width: 600px;
`;

const AttackButton = styled.button<{ disabled: boolean }>`
  padding: 1rem;
  font-size: 1.2rem;
  background-color: ${props => props.disabled ? '#666' : '#ffd700'};
  color: ${props => props.disabled ? '#999' : '#000'};
  border: none;
  border-radius: 8px;
  cursor: ${props => props.disabled ? 'not-allowed' : 'pointer'};
  transition: all 0.3s ease;

  &:hover {
    transform: ${props => props.disabled ? 'none' : 'scale(1.05)'};
  }
`;

interface PlayerProps {
  onAttack: (damage: number) => void;
  disabled: boolean;
}

const Player: React.FC<PlayerProps> = ({ onAttack, disabled }) => {
  return (
    <PlayerContainer>
      {attacks.map((attack, index) => (
        <AttackButton
          key={index}
          onClick={() => onAttack(attack.damage)}
          disabled={disabled}
        >
          {attack.name} (Damage: {attack.damage})
        </AttackButton>
      ))}
    </PlayerContainer>
  );
};

export default Player;