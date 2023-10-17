import styled from "styled-components";
import LogoVertical from './LogoVertical.svg';
import LogoHorizontal from './LogoHorizontal.svg';
import { device } from "../MediaQueries";



export type LogotypeProps = {
  horizontal: boolean
};


const ImageArea = styled.img<{ horizontal?: boolean; }>`
   width: ${props => props.horizontal ? '50%': '70%'};
   margin: ${props => props.horizontal ? '16px': '0px'};

    @media ${device.tablet} { 
      width: ${props => props.horizontal ? '220px': '70%'};
    }
  `;

  export const Logotype: any = ({horizontal}: LogotypeProps) => 
     (<ImageArea horizontal={horizontal} src={!!horizontal?LogoHorizontal:LogoVertical} />) as JSX.Element;
