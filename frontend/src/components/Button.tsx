import styled, {css} from "styled-components";
import { device } from "./MediaQueries";

export const Button = styled.a<{ $primary?: boolean; }>`
  accent-color: white;

  background-color: #B6C13F;
  border-radius: 4px;
 
  color: var(--accent-color);
  display: inline-block;
  margin: 0.5rem 1rem;
  padding: 0.5rem 0;
  transition: all 200ms ease-in-out;
  width: 11rem;
  text-align:center;
  box-shadow: 2px 2px rgba(0, 0, 0, 0.2);

  &:hover {
    filter: brightness(0.85);
  }

  &:active {
    filter: brightness(1);
  }

  ${props => props.$primary && css`
    background: var(--accent-color);
    color: black;
  `}
  font-size:1.2em;
  
  @media ${device.tablet} {
    font-size:1em;
  }
`;


