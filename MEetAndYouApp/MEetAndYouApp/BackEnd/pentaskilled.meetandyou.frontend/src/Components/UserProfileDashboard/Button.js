import React, { Component } from 'react';
import styled from 'styled-components';

function Button({ className, children, ...props }) {
    return (
        <button
            className={className}
            {...props}
        >
            {children}
        </button>
    );
};

export default styled(Button)`
  border: 1px solid #E5E5E5;
  min-height: 50px;
  min-width: 100px;
  background: white;
  color: #404040;
  transition: all 0.25s;
  outline: none;
  &:hover {
    cursor: pointer;
    background: #EDEDED;
  }
  &:focus, &:active {
    background: #DBDBDB;
    border: 1px solid #7F7F7F;
  }
`