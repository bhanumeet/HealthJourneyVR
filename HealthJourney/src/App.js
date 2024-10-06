import React, { useState } from 'react';
import { Container, TextField, Button, Typography, Paper, Box } from '@mui/material';

const App = () => {
  const [input, setInput] = useState('');
  const [chatHistory, setChatHistory] = useState([]);

  const handleSubmit = (e) => {
    e.preventDefault();
    // Here you would typically send the input to your backend API and get a response.
    const response = `You asked: "${input}". (This is where the chatbot response would go.)`;
    setChatHistory([...chatHistory, { question: input, answer: response }]);
    setInput('');
  };

  return (
    <Container maxWidth="sm">
      <Paper elevation={3} style={{ padding: '20px', marginTop: '20px' }}>
        <Typography variant="h5" align="center">Diabetes Chatbot</Typography>
        <Box mt={2}>
          {chatHistory.map((chat, index) => (
            <Box key={index} mb={2}>
              <Typography variant="body1"><strong>You:</strong> {chat.question}</Typography>
              <Typography variant="body1"><strong>Bot:</strong> {chat.answer}</Typography>
            </Box>
          ))}
        </Box>
        <form onSubmit={handleSubmit}>
          <TextField
            label="Ask a question..."
            variant="outlined"
            fullWidth
            value={input}
            onChange={(e) => setInput(e.target.value)}
            style={{ marginBottom: '10px' }}
          />
          <Button type="submit" variant="contained" color="primary" fullWidth>
            Send
          </Button>
        </form>
      </Paper>
    </Container>
  );
};

export default App;
