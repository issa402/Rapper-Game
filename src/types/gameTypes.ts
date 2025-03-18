export interface GameState {
  playerHealth: number;
  opponentHealth: number;
  currentTurn: 'player' | 'opponent';
  gameOver: boolean;
}

export interface Attack {
  name: string;
  damage: number;
}