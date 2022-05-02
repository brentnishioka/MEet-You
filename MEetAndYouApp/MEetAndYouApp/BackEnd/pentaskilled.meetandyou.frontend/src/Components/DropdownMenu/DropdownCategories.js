import React from "react";

import {
    DropdownWrapper,
    StyledSelect,
    StyledOption,
    StyledLabel,
    StyledButton,
} from "./style.js";


export function Dropdown(props) {
    return (
        <DropdownWrapper action={props.action}>
            <StyledLabel htmlFor="categories">
                {props.formLabel}
            </StyledLabel>
            <StyledSelect id="categories" name="categories">
                {props.children}
            </StyledSelect>
            
        </DropdownWrapper>
    );
}

export function Option(props) {
    return (
        <StyledOption selected={props.selected}>
            {props.value}
        </StyledOption>
    );
}