import styled, {css} from "styled-components";
import { device } from "../MediaQueries";


export const Backleft = styled.div`
  z-index: 1000;
  top:0;
  display:flex;
  justify-content:center;
  align-items:center;
  background-color: #105E8A;
  position:absolute;
  width:100%;
  height:50vh;
 
  @media ${device.tablet} { 
    width:45%;
    height:100vh;
  }
  
`;

export const Backright = styled.div`
  position:absolute;
  z-index: 1000;
  display:flex;
  justify-content:center;
  align-items:center;
  width:100%;
  height:50vh;
  top:50%;
  @media ${device.tablet} { 
    top:0;
    right: 0;
    width:55%;
    height:100vh;
  }
`;


export const Backcenter = styled.div<{ 
  $column?: boolean; 
  $alignHL?: boolean; 
  $alignHR?: boolean; 
  $alignHSB?: boolean; 
  $alignHSA?: boolean; 
  $alignVU?: boolean; 
  $alignVD?: boolean; 
  $alignVS?: boolean; 
}>`
  position:absolute;
  z-index: 1000;
  display:flex;
  justify-content:center;
  align-items:center;
  background-color: #105E8A;

  ${props => props.$column && css`
  flex-direction:column;
`}

${props => props.$alignHL && css`
  justify-content: start;
`}

${props => props.$alignHR && css`
  justify-content: end;
`}

${props => props.$alignHSB && css`
  justify-content: space-between;
`}

${props => props.$alignHSA && css`
  justify-content: space-around;
`}

${props => props.$alignVU && css`
  align-items: start;
`}

${props => props.$alignVD && css`
  align-items: end;
`}

${props => props.$alignVS && css`
  align-items: stretch;
`}


  top:120px;
  left:0; 
  width: 100%;
  height: calc(100% - 180px);

  
  @media ${device.tablet} {
    top:100px;
    left:0; 
    width: 100%;
    height: calc(100% - 140px);
    flex-direction:column;
    }
`;



export const Box = styled.div<{ 
  $row?: boolean;
  $column?: boolean; 
  $alignHL?: boolean; 
  $alignHR?: boolean; 
  $alignHSB?: boolean; 
  $alignHSA?: boolean; 
  $alignVU?: boolean; 
  $alignVD?: boolean; 
  $alignVS?: boolean; 
}>`
  position:relative;
  z-index: 1100;
  display:flex;
  justify-content:center;
  align-items:center;
  border-radius: 4px;
  margin:8px;
  background-color: #D9D9D9;

  
  width: 98%;
  height: 100%;
  flex-direction:column;
  
  @media ${device.tablet} {
    left:0; 
    flex-direction:row;
    }
  
  
  
  ${props => props.$column && css`
    flex-direction:column;
  `}

  ${props => props.$row && css`
    flex-direction:row;
  `}

  ${props => props.$alignHL && css`
    justify-content: start;
  `}

  ${props => props.$alignHR && css`
    justify-content: end;
  `}

  ${props => props.$alignHSB && css`
    justify-content: space-between;
  `}

  ${props => props.$alignHSA && css`
    justify-content: space-around;
  `}

  ${props => props.$alignVU && css`
    align-items: flex-start;
  `}

  ${props => props.$alignVD && css`
    align-items: flex-end;
  `}

  ${props => props.$alignVS && css`
    align-items: stretch;
  `}

  

`;

