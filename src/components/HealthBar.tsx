import React from 'react';
import styled from 'styled-components';

const HealthBarContainer = styled.div`
  width: 100%;
  height: 20px;
  background-color: #444;
  border-radius: 10px;
  overflow: hidden;
`;

const HealthFill = styled.div<{ health: number }>`
  width: ${props => props.health}%;
  height: 100%;
  background-color: ${props => {
    if (props.health > 60) return '#4CAF50';
    if (props.health > 30) return '#FFC107';
    return '#F44336';
  }};
  transition: width 0.3s ease-in-out;
`;

interface HealthBarProps {
  health: number;
}

const HealthBar: React.FC<HealthBarProps> = ({ health }) => {
  return (
    <HealthBarContainer>
      <HealthFill health={health} />
    </HealthBarContainer>
  );
};

export default HealthBar;