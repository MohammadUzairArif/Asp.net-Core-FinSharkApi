import React from 'react'

type Props = {}

const Card = (props: Props) => {
  return (
    <div className="card">
        <img src="" alt="" />
        <div className="details">
            <h2>AAPL</h2>
            <p>$100</p>
        </div>
        <p className="info">
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Aperiam esse labore commodi explicabo cumque tempora numquam voluptas eos autem corrupti? Quo velit eveniet corporis totam praesentium optio dicta vero ullam.
        </p>
    </div>
  )
}

export default Card