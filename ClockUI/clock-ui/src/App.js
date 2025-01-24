import React, { useState } from 'react';
import DraggableClockHands from './Components/DraggableClockHands';
import Clock from './Components/Clock';

function App() {
  const [hour, setHour] = useState(3);
  const [minute, setMinute] = useState(0);

  const handleHourChange = (newHour) => {
    setHour(newHour);
  };

  const handleMinuteChange = (newMinute) => {
    setMinute(newMinute);
  };

  const sendTimeToBackend = () => {
    fetch(`/api/clock/hands?hours=${hour}&minutes=${minute}`)
      .then(response => response.json())
      .then(data => console.log('API response:', data))
      .catch(error => console.error('Error:', error));
  };

  return (
    <div>
      <h1>Interactive Clock</h1>
      <Clock />
      <DraggableClockHands
        hour={hour}
        minute={minute}
        onHourChange={handleHourChange}
        onMinuteChange={handleMinuteChange}
      />
      <button onClick={sendTimeToBackend}>Update Time</button>
    </div>
  );
}

export default App;