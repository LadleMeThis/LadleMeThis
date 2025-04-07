import React from 'react';

const CreateFormGroup = ({ label, name, type, value, onChange, placeholder, rows }) => {
  const handleNumberChange = (e) => {
    const newValue = e.target.value;
    if (/^\d*$/.test(newValue) && parseInt(newValue) >= 0) {
      onChange(e);
    }
  };


  return (
    <div className="create-recipe-form-group">
      <label htmlFor={name}>{label}</label>
      {type === "textarea" ? (
        <textarea
          id={name}
          name={name}
          value={value}
          onChange={onChange}
          placeholder={placeholder}
          rows={rows}
          required
        />
      ) : (
        <input
          id={name}
          name={name}
          type={type}
          value={value}
          onChange={type === "number" ? handleNumberChange : onChange}
          placeholder={placeholder}
          min="0"
          required
        />
      )}
    </div>
  );
};

export default CreateFormGroup;