import React from 'react';
import styled from 'styled-components';
import Game from './components/Game';

const AppContainer = styled.div`
  text-align: center;
  min-height: 100vh;
  padding: 20px;
`;

const Title = styled.h1`
  color: #ffd700;
  font-size: 3rem;
  margin-bottom: 2rem;
  text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.5);
`;

function App() {
  return (
    <AppContainer>
      <Title>Rapper Battle Game</Title>
      <Game />
    </AppContainer>
  );
}

export default App;