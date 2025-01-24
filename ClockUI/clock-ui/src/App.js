import React, { useState } from 'react';
import DraggableClockHands from './Components/DraggableClockHands';
import Clock from './Components/Clock';

function App() {
  const [hour, setHour] = useState(3); // Initialize hour to 3 (3 o'clock)
  const [minute, setMinute] = useState(0); // Initialize minute to 0 (00 minutes)
  const [hourDragged, setHourDragged] = useState(false); // Track if hour hand is dragged
  const [minuteDragged, setMinuteDragged] = useState(false); // Track if minute hand is dragged

  const handleHourChange = (newHour) => {
    setHour(newHour);
    setHourDragged(true); // Mark hour hand as dragged
  };

  const handleMinuteChange = (newMinute) => {
    setMinute(newMinute);
    setMinuteDragged(true); // Mark minute hand as dragged
  };

  const sendTimeToBackend = () => {
    fetch(`/api/clock/hands?hours=${hour}&minutes=${minute}`)
      .then(response => response.json())
      .then(data => console.log('API response:', data))
      .catch(error => console.error('Error:', error));
  };

  // Format the time for display (e.g., "03:15")
  const formatTime = (hour, minute) => {
    const formattedHour = Math.round(hour).toString().padStart(2, '0'); // Round and pad
    const formattedMinute = minute.toString().padStart(2, '0'); // Pad minutes
    return `${formattedHour}:${formattedMinute}`;
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
      {/* Display the selected time only if both hands have been dragged */}
      {hourDragged && minuteDragged && (
        <div style={{ margin: '20px 0', fontSize: '24px', fontWeight: 'bold' }}>
          Selected Time: {formatTime(hour, minute)}
        </div>
      )}
      <button onClick={sendTimeToBackend}>Update Time</button>
    </div>
  );
}

export default App;