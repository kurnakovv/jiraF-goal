import React from 'react';
import logo from './logo.svg';
import './App.css';
import Goal from './components/Goal';

function App() {
  return (
    <div className="App">
      <Goal 
        key={'test1'}
        id='test1'
        title='title1'
        description='desc1'
      />
      <Goal 
        key={'test2'}
        id='test2'
        title='title2'
        description='desc2'
      />
      <Goal 
        key={'test3'}
        id='test3'
        title='title3'
        description='desc3'
      />
    </div>
  );
}

export default App;
