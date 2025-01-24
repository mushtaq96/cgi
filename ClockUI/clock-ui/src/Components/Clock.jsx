import React, { useState, useEffect } from 'react';

function Clock() {
  const [time, setTime] = useState(new Date().toLocaleTimeString());

  return <div> The time right now is {time}</div>;
}

export default Clock;
