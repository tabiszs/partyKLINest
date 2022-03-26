import './Heading.css';

interface HeadingProps {
  content: String;
}

const Heading = (props: HeadingProps) => {
  return (
    <div className='heading'>
      {props.content}
    </div>
  );
}

export default Heading;
